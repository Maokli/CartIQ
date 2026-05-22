import { DEFAULT_PAGE_SIZE } from '@domain/base/constants';
import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { PaginationResult } from "@domain/base/paginationResult";
import { Receipt } from "@domain/models/receipt/receipt";
import { ReceiptUseCase } from "@domain/useCases/receipt.usecase";
import { BehaviorSubject, catchError, finalize, Observable, of } from "rxjs";

// Source: https://blog.angular-university.io/angular-material-data-table/
export class ReceiptsDataSource implements DataSource<Receipt> {
  private receiptsSubject =  new BehaviorSubject<Receipt[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  private receiptsCountSubject = new BehaviorSubject(0);

  public loading$ = this.loadingSubject.asObservable();
  public itemsCount$ = this.receiptsCountSubject.asObservable();

  constructor(private receiptUseCase: ReceiptUseCase ) {}

  connect(collectionViewer: CollectionViewer): Observable<readonly Receipt[]> {
    return this.receiptsSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.receiptsSubject.complete();
    this.loadingSubject.complete();
  }

  loadReceipts(pageIndex: number = 0, pageSize: number = DEFAULT_PAGE_SIZE) {
    this.loadingSubject.next(true);

    this.receiptUseCase.getReceipts(pageIndex, pageSize)
      .pipe(
        catchError(() => of({} as PaginationResult<Receipt>)),
        finalize(() => this.loadingSubject.next(false))
      )
      .subscribe((res) => {
        this.receiptsSubject.next(res.items);
        this.receiptsCountSubject.next(res.itemsCount);
      });
  }

}
