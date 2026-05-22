import { Routes } from '@angular/router';
import { ReceiptsList } from '@presentation/receipt/receipts-list/receipts-list';

export const routes: Routes = [
  {
    path: "receipts",
    loadChildren: () => import('@presentation/receipt/receipt-module').then(m => m.ReceiptModule)
  }
];
