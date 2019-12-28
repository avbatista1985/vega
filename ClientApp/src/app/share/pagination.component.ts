import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styles: [
	  
  ]
})
export class PaginationComponent implements OnInit {
  	@Input('total-items') totalItems;
	@Input('page-size') pageSize;
  	@Output('page-changed') pageChanged = new EventEmitter();
  pages: any[];
	currentPage = 1; 
  constructor() { }

  ngOnInit() {
    this.currentPage = 1;
        
		var pagesCount = Math.ceil(this.totalItems / this.pageSize); 
		this.pages = [];
		for (var i = 1; i <= pagesCount; i++)
			this.pages.push(i);

  //  console.log(this);
  }
  ngOnChanges(){
    this.currentPage = 1;
        
		var pagesCount = Math.ceil(this.totalItems / this.pageSize); 
		this.pages = [];
		for (var i = 1; i <= pagesCount; i++)
			this.pages.push(i);

  //  console.log(this);
	}

	changePage(page){
		this.currentPage = page; 
		this.pageChanged.emit(page);
	}

	previous(){
		if (this.currentPage == 1)
			return;

		this.currentPage--;
		this.pageChanged.emit(this.currentPage);
	}

	next(){
		if (this.currentPage == this.pages.length)
			return; 
		
		this.currentPage++;
   // console.log("next", this);
		this.pageChanged.emit(this.currentPage);
	}
}
/** */