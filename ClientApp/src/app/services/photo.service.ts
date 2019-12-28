import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {

  constructor(private http:HttpClient) { }

  upload(vehicleId,photo){
    var formData= new FormData();
    formData.append('file',photo);

    //return this.http.post('https://localhost:44393/api/vehicles/'+vehicleId+'/photos',file);
    return this.http.post(`https://localhost:44393/api/vehicles/${vehicleId}/photos`,formData,{
      reportProgress: true,
      observe: 'events'
    });
        
  }

  getPhotos(vehicleId){
    return this.http.get(`https://localhost:44393/api/vehicles/${vehicleId}/photos`);

  }
}
