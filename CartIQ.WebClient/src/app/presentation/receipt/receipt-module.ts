import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceiptRepository } from '@domain/repositories/receipt.repository';
import { ReceiptUseCase } from '@domain/useCases/receipt.usecase';
import { ReceiptRepositoryImpl } from '@data/receipt/repositories/receiptImplementation.repository';
import { ReceiptUseCaseImpl } from '@data/receipt/useCases/receiptImplementation.usecase';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { ReceiptsList } from './receipts-list/receipts-list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { provideRouter } from '@angular/router';
import { receiptRoutes } from './receipt.routes';
import { SharedModule } from '@presentation/shared/shared-module';
import { ReceiptsPage } from './receipts-page/receipts-page';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [ReceiptsList, ReceiptsPage],
  imports: [CommonModule, SharedModule, MatTableModule, MatPaginatorModule, MatProgressSpinnerModule, MatIconModule],
  providers: [
    {
      provide: ReceiptRepository,
      useClass: ReceiptRepositoryImpl
    },
    {
      provide: ReceiptUseCase,
      useClass: ReceiptUseCaseImpl
    },
    provideRouter(receiptRoutes)
  ]
})
export class ReceiptModule {}
