export interface OrderCreate {
    empid: number;
    shipperid: number;
    shipname: string;
    shipaddress: string;
    shipcity: string;
    orderdate: string;
    requireddate: string;
    shippeddate: string;
    freight: number;
    shipcountry: string;
    detail: OrderDetail[];
}
  
    
export interface OrderDetail {
    productid: number;
    unitprice: number;
    qty: number;
    discount: number;
}

