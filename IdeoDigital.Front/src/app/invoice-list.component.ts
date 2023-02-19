import { AfterViewInit, Component, Inject , OnInit, ViewChild } from '@angular/core';
import { InvoiceService, Invoice } from './invoice-service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NewInvoiceDialogeComponent } from './new-invoice-dialoge/new-invoice-dialoge.component';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit,AfterViewInit {
  invoices;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public displayedColumns = ['id', 'supplierName', 'customerName', 'date', 'dueDate', 'subTotal', 'invoiceStatus'];
  public clickedRows = new Set<Invoice>();
  public dataSource = new MatTableDataSource<Invoice>();
  constructor(public dialog: MatDialog, private invoiceService: InvoiceService) { }

  ngOnInit(): void {
    this.getInvoices();
  }
  ngAfterViewInit() {
      this.dataSource.paginator = this.paginator;
  }

  private getInvoices() {
    this.invoiceService.get('Invoice')
      .subscribe(invoices => {
        //this.invoices = invoices;
        this.dataSource.data = invoices;
        console.log(this.dataSource.data);
      });
  }
  public onRowClick(row) {
    //This is for editing
    console.log(row.id);
    this.dialog.open(NewInvoiceDialogeComponent, {
      data: {
        action: 'invoiceDetails',
        id: row.id
      },
    });
  }
  public raisePopUpInvoice() {
    this.dialog.open(NewInvoiceDialogeComponent, {
      data: {
        action: 'createInvoice',
      },
    });
  }
}
