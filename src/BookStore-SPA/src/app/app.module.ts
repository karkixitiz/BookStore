import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BookService } from './services/book.service';
import { CategoryService } from './services/category.service';
import { ConfirmationDialogService } from './services/confirmation-dialog.service';
import { CategoryComponent } from './categories/category/category.component';
import { CategoryListComponent } from './categories/category-list/category-list.component';
import { BookComponent } from './books/book/book.component';
import { BookListComponent } from './books/book-list/book-list.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { Datepicker  } from './datepicker/datepicker.component';

@NgModule({
	declarations: [
		AppComponent,
		CategoryComponent,
		CategoryListComponent,
		BookComponent,
		BookListComponent,
		HomeComponent,
		NavComponent,
		ConfirmationDialogComponent,
		Datepicker
	],
	imports: [
		BrowserModule,
		AppRoutingModule,
		NgbModule,
		HttpClientModule,
		FormsModule,
		BrowserAnimationsModule,
		ToastrModule.forRoot()
	],
	providers: [
		BookService,
		CategoryService,
		ConfirmationDialogService,
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
