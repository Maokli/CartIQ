import { Nutrients } from "../shared/nutrients";

export interface Receipt {
  id: string;
  created: Date;
  merchant: Merchant;
  currency?: string;
  totalAmount?: number;
  itemsCount: number;
  nutrients: Nutrients;
  category: string;
  topItems: string[];
}

interface Merchant {
  name: string,
  backgroundColor: string,
  color: string
}
