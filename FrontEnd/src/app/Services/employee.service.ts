import { Injectable } from '@angular/core';

import{HttpClient} from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Employee } from '../Interfaces/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

private endpoint:string = environment.endPoint;
private apiUrl:string = this.endpoint + "Employee/"

  constructor(private http:HttpClient) { }

  getList():Observable<Employee[]>{
    return this.http.get<Employee[]>(`${this.apiUrl}`);
  }
}
