import { ReceiptItem } from "./receiptItem";

export interface ReceiptDetails {
  id?: string;
  merchant?: string;
  currency?: string;
  items?: ReceiptItem[];
}
