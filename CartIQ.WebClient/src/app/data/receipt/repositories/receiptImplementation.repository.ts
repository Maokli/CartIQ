import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReceiptRepository } from '../../../domain/repositories/receipt.repository';
import { ReceiptDetailsResult, ReceiptListResult } from '../../../domain/models/receipt/types';
import { BooleanResult } from '../../../domain/base/types';
import { UpdateReceiptCommand } from '../../../domain/models/receipt/updateReceiptCommand';


@Injectable()
export class ReceiptRepositoryImpl extends ReceiptRepository {
  private readonly baseUrl = 'http://localhost:5200/api/Receipts';

  constructor(private http: HttpClient) {
    super();
  }

  getReceipts(pageIndex: number, pageSize: number): Observable<ReceiptListResult> {
    const params = new HttpParams()
      .set('pageIndex', pageIndex)
      .set('pageSize', pageSize);

    return this.http.get<ReceiptListResult>(this.baseUrl, { params });
  }

  getReceiptDetails(id: string): Observable<ReceiptDetailsResult> {
    return this.http.get<ReceiptDetailsResult>(`${this.baseUrl}/${id}`);
  }

  deleteReceipt(id: string): Observable<BooleanResult> {
    return this.http.delete<BooleanResult>(`${this.baseUrl}/${id}`);
  }

  updateReceipt(command: UpdateReceiptCommand): Observable<BooleanResult> {
    return this.http.patch<BooleanResult>(`${this.baseUrl}/Update`, command);
  }
}
