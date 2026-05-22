import { ReceiptItem } from "./receiptItem";

export interface UpdateReceiptCommand {
  id: string;
  merchant: string;
  currency: string;
  itemsToDelete?: ReceiptItem[];
  itemsToAdd?: ReceiptItem[];
}
