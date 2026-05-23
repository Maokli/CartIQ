export interface PaginationResult<T> {
  pageIndex: number;
  totalPages: number;
  pageSize: number;
  itemsCount: number;
  items: T[];
}
