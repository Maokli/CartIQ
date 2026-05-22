import { PaginationResult } from "../../base/paginationResult";
import { Result } from "../../base/result";
import { Receipt } from "./receipt";
import { ReceiptDetails } from "./receiptDetails";

export type ReceiptListResult = Result<PaginationResult<Receipt>>;
export type ReceiptDetailsResult = Result<ReceiptDetails>;
