import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, take } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private baseUrl: string;
  constructor(private http: HttpClient) {
    this.baseUrl = environment.apiUrl;
  }
  get(controller) {
    let url = this.baseUrl + controller;
    return this.http.get<Invoice[]>(url);
  }
  getById(controller, id) {
    let url = this.baseUrl + controller + '/' + id;
    return this.http.get<Invoice>(url);
  }
  post(controller, newInvoice: NewInvoiceResponse) {
    let url = this.baseUrl + controller;
    return this.http.post(url, newInvoice)
      .pipe(map(response => {
        return response;
      }));
  }
  put(controller, newInvoice: NewInvoiceResponse) {
    let url = this.baseUrl + controller;
    return this.http.put(url, newInvoice)
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