import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'receipts',
    loadChildren: () => import('@presentation/receipt/receipt.routes').then((m) => m.receiptRoutes),
  },
];
