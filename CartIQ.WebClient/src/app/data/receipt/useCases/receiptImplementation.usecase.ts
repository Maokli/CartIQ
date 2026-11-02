import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ReceiptUseCase } from '../../../domain/useCases/receipt.usecase';
import { ReceiptRepository } from '../../../domain/repositories/receipt.repository';
import { PaginationResult } from '../../../domain/base/paginationResult';
import { Receipt } from '../../../domain/models/receipt/receipt';
import { ReceiptDetails } from '../../../domain/models/receipt/receiptDetails';
import { UpdateReceiptCommand } from '../../../domain/models/receipt/updateReceiptCommand';


@Injectable()
export class ReceiptUseCaseImpl extends ReceiptUseCase {
  constructor(private repo: ReceiptRepository) {
    super();
  }

  getReceipts(
    pageIndex: number,
    pageSize: number
  ): Observable<PaginationResult<Receipt>> {
    return this.repo.getReceipts(pageIndex, pageSize).pipe(
      map(res => {
        if (!res.resultItem) {
          throw new Error('Failed to fetch receipts');
        }

        return res.resultItem;
      })
    );
  }

  getReceiptDetails(id: string): Observable<ReceiptDetails | null> {
    return this.repo.getReceiptDetails(id).pipe(
      map(res => res.resultItem)
    );
  }

  deleteReceipt(id: string): Observable<boolean> {
    return this.repo.deleteReceipt(id).pipe(
      map(res => res.resultItem ?? false)
    );
  }

  updateReceipt(command: UpdateReceiptCommand): Observable<boolean> {
    return this.repo.updateReceipt(command).pipe(
      map(res => res.resultItem ?? false)
    );
  }
}
