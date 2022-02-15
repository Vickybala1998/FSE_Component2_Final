
import { TweetappService } from './tweetapp.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import{FormsModule,ReactiveFormsModule} from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MatDialogModule} from '@angular/material/dialog'
import { HashtagMentionColLibModule } from 'hashtag-mention-colorizer';;

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AllusersComponent } from './allusers/allusers.component';
import { RegistrationComponent } from './registration/registration.component';
import { TweetComponent } from './tweet/tweet.component';
import { ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AllusersComponent,
    RegistrationComponent,
    TweetComponent,
    ForgotpasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MDBBootstrapModule,
    RouterModule,
    HttpClientModule,
    MatDialogModule,
    HashtagMentionColLibModule
  ],
  providers: [TweetappService,HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
