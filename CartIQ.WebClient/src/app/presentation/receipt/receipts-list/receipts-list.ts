import { DEFAULT_PAGE_SIZE } from './../../../domain/base/constants';
import { ReceiptUseCase } from '@domain/useCases/receipt.usecase';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from "@angular/material/paginator";
import { ReceiptsDataSource } from './receipts-data-source';
import { tap } from 'rxjs';
import { Receipt } from '@domain/models/receipt/receipt';

@Component({
  selector: 'app-receipts-list',
  templateUrl: './receipts-list.html',
  styleUrl: './receipts-list.scss',
  standalone: false
})
export class ReceiptsList implements OnInit, AfterViewInit {
  displayedColumns = ['merchant', 'category', 'items', 'nutrients', 'totalAmount', 'actions'];
  dataSource: ReceiptsDataSource;

  // TO NE USED IN VIEW
  DEFAULT_PAGE_SIZE = DEFAULT_PAGE_SIZE;

  constructor(private receiptUseCase: ReceiptUseCase) {
    this.dataSource = new ReceiptsDataSource(receiptUseCase);
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit() {
    this.dataSource.loadReceipts();
  }

  ngAfterViewInit() {
    this.paginator.page
      .pipe(
        tap(() => this.loadReceiptsPage())
      )
      .subscribe();
  }

  loadReceiptsPage() {
    this.dataSource.loadReceipts(this.paginator.pageIndex, this.paginator.pageSize)
  }

  asReceipt(item: any): Receipt {
    return item as Receipt;
  }
}
