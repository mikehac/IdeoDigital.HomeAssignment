import { Component, OnInit } from '@angular/core';
import { InvoiceService, Invoice } from './invoice-service';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {
  invoices;
  public displayedColumns = ['id', 'supplierName','customerName', 'date', 'dueDate', 'subTotal', 'invoiceStatus'];
  public dataSource = new MatTableDataSource<Invoice>();
  constructor(private invoiceService: InvoiceService) { }

  ngOnInit(): void {
    this.getInvoices();
  }
  private getInvoices() {
    this.invoiceService.get('Invoice')
      .subscribe(invoices => {
        //this.invoices = invoices;
        this.dataSource.data = invoices;
        console.log(this.dataSource.data);
      });
  }
}
