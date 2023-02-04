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
  post(controller, newInvoice: NewInvoiceResponse) {
    let url = this.baseUrl + controller;
    return this.http.post(url, newInvoice)
      .pipe(map(response => {
        return response;
      }));
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
export interface NewInvoiceResponse {
  customerName: string,
  customerAddress: string,
  supplierName: string,
  supplierAddress: string,
  date: Date,
  dueDate: Date,
  subTotal: number,
  // items: Item[],
  invoiceStatus: string
}
interface Item{
  description: string,
  quentity: number,
  rate: number  
}