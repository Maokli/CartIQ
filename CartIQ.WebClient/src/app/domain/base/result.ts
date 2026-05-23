export interface Result<T> {
  succeeded: boolean;
  errors?: string[];
  resultItem: T | null;
}
