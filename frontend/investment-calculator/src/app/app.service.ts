import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders }    from '@angular/common/http';  
@Injectable({
  providedIn: 'root'
})
export class AppService {  
  readonly rootURL = 'https://localhost:49157/api';
  constructor(private http: HttpClient) { }  
      httpOptions = {  
        headers: new HttpHeaders({  
          'Content-Type': 'application/json',
        })  
      }          
      postData(data : any){  
        return this.http.post(this.rootURL + '/cdb/calculate',data);  
      }   
}