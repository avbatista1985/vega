import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair, QueryResult } from '../models/vehicle.model';
import { VehicleService } from '../services/vehicle.service';

@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styles: []
})
export class VehiclesListComponent implements OnInit {
   queryResult:QueryResult={
     totalItems:0,
     items:[]
   };
   asdasd:any;
   allVehicles;
   vehicles;
   makes;
   totalItems=0;
   query:any ={};
   column=[
     {title:'Id'},
     {title:'Make', Key:'make', isSortable:true},
     {title:'Model', Key:'model', isSortable:true},
     {title:'Contact Name', Key:'contactName', isSortable:true},
     {}
   ];
  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(makes=> this.makes=makes);
      this.resetFilter();
      this.populateVehicle();
    //.subscribe(x=>this.vehiclesList=this.allVehicles=x);
  }
  private populateVehicle(){
    console.log(this.query);
    console.log(this.queryResult);
    console.log("this.queryResult");
    this.vehicleService.getVehicles(this.query)
    .subscribe(result=>{
        this.queryResult.totalItems=(result as QueryResult).totalItems ;
        this.queryResult.items=(result as QueryResult).items;
        this.vehicles=this.queryResult.items;
        this.totalItems=this.queryResult.totalItems;  
        }
      );

//.subscribe(result=>this.asdasd= result);

  }
  onFilterChange(){
    this.query.page=1;
    this.populateVehicle();
    /* var vehicles=this.queryResult.items;

     if(this.query.makeId)
     vehicles= vehicles.filter(v=> v.make.id == this.query.makeId);

     this.vehicles=vehicles;*/
    // this.totalItems=vehicles.length;
  }
  resetFilter(){
    this.query={
      page:1,
      pageSize:5
    };
    this.populateVehicle();
  }
  sortBy(columnName){
    if(this.query.sortBy=== columnName)
      if(this.query.isSortAscending==false)
        this.query.isSortAscending=true;
      else
      this.query.isSortAscending=false;
    else
    {
      this.query.sortBy=columnName;
      this.query.isSortAscending=true;
    }
    console.log(columnName);
    this.populateVehicle();
  }
  onPageChange(page){
    this.query.page=page;
    this.populateVehicle(); 
  }
}
