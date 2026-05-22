import { Observable } from "rxjs";
import { ReceiptDetailsResult, ReceiptListResult } from "../models/receipt/types";
import { UpdateReceiptCommand } from "../models/receipt/updateReceiptCommand";
import { BooleanResult } from "../base/types";

export abstract class ReceiptRepository {
  abstract getReceipts(pageIndex: number, pageSize: number): Observable<ReceiptListResult>;

  abstract getReceiptDetails(id: string): Observable<ReceiptDetailsResult>;

  abstract deleteReceipt(id: string): Observable<BooleanResult>;

  abstract updateReceipt(command: UpdateReceiptCommand): Observable<BooleanResult>;
}
