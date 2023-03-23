import { Component, OnInit, Inject } from '@angular/core';

import { FormBuilder, FormGroup, Validators, FormArray, FormControl, FormGroupName } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import {MatGridListModule} from '@angular/material/grid-list';

import { MatSnackBar } from '@angular/material/snack-bar';

import {MAT_DATE_FORMATS} from '@angular/material/core'
import * as moment from 'moment';

import { Employee } from 'src/app/Interfaces/employee';
import { Shipper } from 'src/app/Interfaces/shipper';
import { Product } from 'src/app/Interfaces/product';
import { OrderCreate, OrderDetail } from 'src/app/Interfaces/order-create'; 

import { EmployeeService } from 'src/app/Services/employee.service';
import { ShipperService } from 'src/app/Services/shipper.service';
import { ProductService } from 'src/app/Services/product.service';
import { OrderService } from 'src/app/Services/order.service';
import { OrderList } from 'src/app/Interfaces/order-list';

export const MY_DATE_FORMATS = {
  parse:{
    dateInput: 'DD/MM/YYYY',
  },
  display:{
    dateInput:'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel:'LL',
    monthYearA11yLabel:'MMMM YYYY'
  }
}

@Component({
  selector: 'app-dialog-add-order',
  templateUrl: './dialog-add-order.component.html',
  styleUrls: ['./dialog-add-order.component.css'],
  providers:[
    {provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS}
  ]
})
export class DialogAddOrderComponent implements OnInit {
  Orderform:FormGroup;
  customerName:string = "";
  customerId:number = 0;
  titleAction:string = "New";
  buttonAction:string ="Save";
  employeeList: Employee[]=[];
  shipperList: Shipper[]=[];
  productList: Product[]=[];
  orderdetailWP: OrderDetail[]=[]

  orderDetailForm = this.fb.group({
    productId: ['']
      //,
      //unitprice: [''],
      //qty: [''],
      //discount: ['']
  })




  constructor(
    private referenceDialog: MatDialogRef<DialogAddOrderComponent>,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _employeeService: EmployeeService,
    private _shipperService: ShipperService,
    private _productService: ProductService,
    private _orderService: OrderService,
    @Inject (MAT_DIALOG_DATA) public dataOrder:OrderList
    ) 
    {


      this.Orderform = this.fb.group({
        empid: [''], //Validators.required],
        shipperId: [''],
        shipName: [''],
        shipAddress: [''],
        shipCity: [''],
        orderDate: [''],
        requiredDate: [''],
        shippedDate :[''],
        freight: [''],
        shipCountry: [''],
        productId: [''],
        unitprice: [''],
        quantity: [''],
        discount: [''],
        orderdetails: this.fb.array([])
      })

      this._employeeService.getList().subscribe({
        next:(dataResponse) => {
          this.employeeList = dataResponse;
        },error:(e) => { console.log(e)}
      }); 

      this._shipperService.getList().subscribe({
        next:(dataResponse) => {
          this.shipperList = dataResponse;
        },error:(e) => { console.log(e)}
      });
      
      this._productService.getList().subscribe({
        next:(dataResponse) => {
          this.productList = dataResponse;
        },error:(e) => { console.log(e)}
      });    
    }
  
    get orderdetails() {
      return this.Orderform.controls["orderdetails"] as FormArray;
    }
    
    addOrderDetail() {    
      console.log('paso ' + this.orderDetailForm)

      this.orderdetails.push(this.orderDetailForm);
    }
    
    deleteOrderDetail(orderDetailIndex: number) {
      this.orderdetails.removeAt(orderDetailIndex);
  }


  showAlert(message: string, action: string) {
      this._snackBar.open(message, action, {
        horizontalPosition:"end",
        verticalPosition:"top",
        duration: 3000
      });
  }

  addOrder()
  {
    debugger;
    console.log(this.Orderform.value)

    //PARA usar encima del boton
    // [disable]="Orderform.ivalid"

    const pepe: OrderDetail = {
      productid: this.Orderform.value.productId,
      unitprice: Number(this.Orderform.value.unitprice),
      qty: Number(this.Orderform.value.quantity),
      discount: Number(this.Orderform.value.discount)
    }  

    this.orderdetailWP.push(pepe);

    const modelo: OrderCreate = {
      empid: this.Orderform.value.empid,
      shipperid: this.Orderform.value.shipperId,
      shipname: this.Orderform.value.shipName,
      shipaddress: this.Orderform.value.shipAddress,
      shipcity: this.Orderform.value.shipCity,
      orderdate: moment(this.Orderform.value.orderdate).format("YYYY-MM-DD"),
      requireddate:  moment(this.Orderform.value.requireddate).format("YYYY-MM-DD"),
      shippeddate : moment(this.Orderform.value.shippeddate).format("YYYY-MM-DD"),
      freight: Number(this.Orderform.value.freight),
      shipcountry: this.Orderform.value.shipCountry,
      detail: this.orderdetailWP //this.Orderform.value.orderDetails.array([])
      }

      this._orderService.create(modelo).subscribe({ 
        next:(data) => {
          this.showAlert("Order created", "Ok");
          this.referenceDialog.close("created")
        },error:(e) => { 
          this.showAlert("Not Ceated", "Error");
        } 
      });  

    }

    ngOnInit(): void {
      if(this.dataOrder){
        this.customerName = this.dataOrder.customerName;
        this.customerId = this.dataOrder.custId;
      }
    }
  }

