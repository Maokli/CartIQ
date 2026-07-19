import { Routes } from '@angular/router';
import { ReceiptRepository } from '@domain/repositories/receipt.repository';
import { ReceiptUseCase } from '@domain/useCases/receipt.usecase';
import { ReceiptRepositoryImpl } from '@data/receipt/repositories/receiptImplementation.repository';
import { ReceiptUseCaseImpl } from '@data/receipt/useCases/receiptImplementation.usecase';
import { ReceiptsPage } from './receipts-page/receipts-page';

export const receiptRoutes: Routes = [
  {
    path: '',
    component: ReceiptsPage,
    providers: [
      { provide: ReceiptRepository, useClass: ReceiptRepositoryImpl },
      { provide: ReceiptUseCase, useClass: ReceiptUseCaseImpl },
    ],
  },
];
