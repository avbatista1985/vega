import { Component, OnInit, ErrorHandler } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { SaveVehicle, Vehicle, SendVehicle } from '../models/vehicle.model';
import { forEach } from '@angular/router/src/utils/collection';


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes;

  modelId:any[];
  vehicleFeatures:any;
  Cont:String= "asdasd";
  vehicleNew:any={}
  veii:SendVehicle={
    modelId: 0,
    isRegistered: false,
    vehicleFeatures:[],
    Contact:{
      Name: '',
      Email:'',
      Phone:''
    }
  };

  vehicle: SaveVehicle={
    id: 0, 
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    vehicleFeatures:[],
    Contact:{
      Name: '',
      Email:'',
      Phone:''
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router:Router,
    private vehicleService:VehicleService,
    private toastr: ToastrService) { 

      route.params.subscribe( p => {
        this.vehicle.id=+p['id'];
      })
    }

  ngOnInit() {
    var source=[
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()
    ];
if(this.vehicle.id)
    source.push(this.vehicleService.getVehicle(this.vehicle.id));

    forkJoin(source).subscribe(data=>{
        this.makes=data[0];
        console.log("makes");
        console.log(this.makes);
        this.vehicleFeatures=data[1];
        if(this.vehicle.id)
        {
          this.setVehicle(data[2]);
          this.populateModels();
        }
         console.log(this.vehicle);
    }, err=>{
      if(err.status==500)
     console.log("dddddddddddddddddddddddddddddd");
        this.router.navigate(['']);
    });


    //this.vehicleService.getFeatures().subscribe(features =>
    //  this.vehicleFeatures= features);
  }

    private setVehicle(v){
    this.vehicle.id=v.id;
    this.vehicle.makeId=v.make.id;
    this.vehicle.modelId=v.model.id;
    this.vehicle.isRegistered=v.isRegistered;
    this.vehicle.Contact.Email=v.contact.email;
    this.vehicle.Contact.Name=v.contact.name;
    this.vehicle.Contact.Phone=v.contact.phone;
        v.vehicleFeatures.forEach(feach => this.vehicle.vehicleFeatures.push(feach.id));
  }

  onMakeChange(){
   this.populateModels();
   delete this.vehicle.modelId;
   console.log("asdsadasd", this.vehicle.makeId);
   
   //console.log("asdsadasd", this.models);
  }

  private populateModels(){
    var selectedMakes= this.makes.find(m=>m.id==this.vehicle.makeId); 
   this.modelId= selectedMakes ? selectedMakes.models:[];
  }
  onFeatureToggle(featuresId, $event){
    if($event.target.checked){
      this.vehicle.vehicleFeatures.push(featuresId);
      console.log("a","a");}

    else{
      console.log("b","b");
      var index= this.vehicle.vehicleFeatures.indexOf(featuresId);
      this.vehicle.vehicleFeatures.splice(index,1); } 
  }
  submit(){

    console.log("asdasdasdasdasdasdasd");
    if(this.vehicle.id)
    {
      console.log("ccccccccccccccccc");
      this.vehicleService.updateVehicle(this.vehicle.id,this.vehicle)
      .subscribe(x=>{
        this.toastr.success("The vehicles was sucessfuly Update.");
        this.router.navigate(['/vehicles/'+this.vehicle.id]);
      });
    }
    else{
      this.veii.modelId=this.vehicle.modelId;
      this.veii.isRegistered=this.vehicle.isRegistered;
      this.veii.vehicleFeatures=this.vehicle.vehicleFeatures;
      this.veii.Contact=this.vehicle.Contact;
      console.log(this.veii);
      this.vehicleService.newVehicle(this.veii)
      .subscribe(x=>{
        this.vehicleNew=x;
        console.log(x);
        this.router.navigate(['/vehicles/'+this.vehicleNew.id]);
      }
      );
    }
  }
  delete(){
    if(confirm("Are you sure")){
      this.vehicleService.delete(this.vehicle.id)
      .subscribe(x=>{
        this.toastr.success("The vehicle was deleted");
        this.router.navigate(['']);
      })
    }
  }
} 
