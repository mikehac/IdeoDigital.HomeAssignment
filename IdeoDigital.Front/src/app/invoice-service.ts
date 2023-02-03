import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, take } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private baseUrl = 'http://localhost:5197/api/';
  constructor(private http: HttpClient) { }
  get(controller) {
    let url = this.baseUrl + controller;
    return this.http.get<Invoice[]>(url);
  }
}
export interface Invoice {
  id: number,
  customerId: number,
  customerName: string,
  supplierName: string,
  date: Date,
  dueDate: Date,
  subTotal: Float32Array,
  statusId: number,
  invoiceStatus: string
}
interface InvoiceResponse {
  invoices: Invoice[]
}