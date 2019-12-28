import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { VehicleService } from '../services/vehicle.service';
import {ContentChildren, Directive, Input, QueryList} from '@angular/core';
import { v } from '@angular/core/src/render3';
import { PhotoService } from '../services/photo.service';
import { Photo } from '../models/vehicle.model';
import { isType } from '@angular/core/src/type';
import { HttpEvent, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styles: []
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicle:any={
    make:{},
    model:{},
    contact:{},
    vehicleFeatures:[]
  };
  vehicleId:number; 
  photos;
  fileName:string="Chosse File";
  nativeElement:HTMLInputElement;
  progresss:number=-1;
  constructor(
    private route: ActivatedRoute,
    private router:Router,
    private photoService:PhotoService,
    private vehicleService:VehicleService,
    private toastr: ToastrService) {
      route.params.subscribe(
        p=>{
          this.vehicleId=+p['id'];
          if(isNaN(this.vehicleId)||this.vehicleId<=0){
            router.navigate(['/vechicle']);
            return;
          }
        }
      ) 
     }

  ngOnInit() {
    
      this.vehicleService.getVehicle(this.vehicleId)
    .subscribe(
      v=>{this.vehicle=v;
        this.loadphoto();
      },
      err=>{
        if(err.status==404){
          this.router.navigate(['/vehicles']);
          return;
        };
        if(err.status==500){
          this.router.navigate(['/vehicles']);
          this.toastr.error(err.message);
          return;
        };
console.log(this.vehicle);}
    )
  }
 delete(){
   if(confirm("Are you sure???")){
     this.vehicleService.delete(this.vehicleId)
     .subscribe(
       x=>{
        this.router.navigate(['/vehicles']);
       }
     )
   }
 }
 selectPhoto(){
  this.nativeElement =  this.fileInput.nativeElement;
  this.fileName=this.nativeElement.files[0].name;
 }
 uploadPhoto(){
   if(this.nativeElement){
      this.progresss=0;
      this.photoService.upload(this.vehicleId, this.nativeElement.files[0])
      .subscribe((event:HttpEvent<any>)=>
        {
          switch (event.type) {
            case HttpEventType.Sent:
              console.log('Request has been made!');
              break;
            case HttpEventType.ResponseHeader:
              console.log('Response header has been received!');
              break;
            case HttpEventType.UploadProgress:
              this.progresss = Math.round(event.loaded / event.total * 100);
              console.log(`Uploaded! ${this.progresss}%`);
             break;
            case HttpEventType.Response:
              console.log("aaaaaaaaaaaaaaaaaaaaa"); 
              this.photos.push(event.body);
              this.fileInput.nativeElement.value="",
              this.nativeElement=null;
              this.fileName="Chosse File"; 

              setTimeout(() => {
                this.progresss =-1;
              }, 1500);
            break;
          
            default:
              break;
          }
      }
      );
    }
    else
    this.toastr.error("You must select a Photo");
 }
 private loadphoto(){
  this.photoService.getPhotos(this.vehicleId)
  .subscribe(photos=>{this.photos=photos;
     console.log("adadad");
      console.log(photos); 
    }
    );
 }
}
