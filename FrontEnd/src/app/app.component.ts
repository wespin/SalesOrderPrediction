import {AfterViewInit, Component, ViewChild, OnInit} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';

import { OrderList } from './Interfaces/order-list';
import { OrderService } from './Services/order.service';

import {MatDialog} from '@angular/material/dialog';
import {DialogAddOrderComponent} from './Dialogs/dialog-add-order/dialog-add-order.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnInit {
  title = 'FrontEnd';
  displayedColumns: string[] = ['custid', 'customername', 'lastorderdate', 'nextpredictedorder', 'vieworders', 'neworder'];
  dataSource = new MatTableDataSource<OrderList>();

  constructor(private _orderService: OrderService, public dialog: MatDialog){}


  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnInit(): void {
    this.showOrders()
  }

  showOrders(){
    this._orderService.getPrediction().subscribe({
        next:(dataResponse) => {
          this.dataSource.data = dataResponse;
      },error:(e) => { console.log(e)}
    }); 
  }

  openDialog() {
    this.dialog.open(DialogAddOrderComponent, {
      disableClose: true
      ,height: '1000px',
      width: '800px',
    }).afterClosed().subscribe(result =>{
      if(result === "create"){
        this.showOrders();
      }
    })
  };

}
