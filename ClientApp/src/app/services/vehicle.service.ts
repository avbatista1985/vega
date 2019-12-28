import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType } from "@angular/common/http";
import { filter, map } from 'rxjs/operators';

//import "rxjs/add/operator/map";
@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http:HttpClient) { }
  getMakes(){
    return this.http.get('https://localhost:44393/api/makes')
    //.map(res=>res.json());
  }

  getFeatures(){
    return this.http.get('https://localhost:44393/api/features')
    //.map(res=>res.json());
  }
  newVehicle(vehicle){
    return this.http.post('https://localhost:44393/api/vehicles',vehicle);
    //.map(res=>res.json());
  }
  getVehicle(id){
    return this.http.get('https://localhost:44393/api/vehicles/'+id); 
    //.map(res=> res.json);
  }
  getVehicles(filter){
    return this.http.get('https://localhost:44393/api/vehicles?'+this.toQueryString(filter));
    //.map(res=> res.json);
  }

  toQueryString(obj){
    var parts=[];
    for( var property in obj){
      var value=obj[property];
      if(value!= null && value !=undefined)
        parts.push(encodeURIComponent(property)+'='+encodeURIComponent(value));
    }
    console.log(parts.join("&"));
    return parts.join("&");
  }
  updateVehicle(id,vehicle){
    return this.http.put('https://localhost:44393/api/vehicles/'+id,vehicle);
    //.map(res=> res.json);
  }
  delete(id){
    return this.http.delete('https://localhost:44393/api/vehicles/'+id);
    //.map(res=>res.json);
  }
}
