import { Observable } from 'rxjs';
import { PaginationResult } from '../base/paginationResult';
import { Receipt } from '../models/receipt/receipt';
import { ReceiptDetails } from '../models/receipt/receiptDetails';
import { UpdateReceiptCommand } from '../models/receipt/updateReceiptCommand';

export abstract class ReceiptUseCase {
  abstract getReceipts(
    pageIndex: number,
    pageSize: number
  ): Observable<PaginationResult<Receipt>>;

  abstract getReceiptDetails(
    id: string
  ): Observable<ReceiptDetails | null>;

  abstract deleteReceipt(id: string): Observable<boolean>;

  abstract updateReceipt(command: UpdateReceiptCommand): Observable<boolean>;
}
