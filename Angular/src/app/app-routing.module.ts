import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import{LoginComponent} from './login/login.component';
import{RegistrationComponent} from './registration/registration.component';
import{ ForgotpasswordComponent } from './forgotpassword/forgotpassword.component';
import{ TweetComponent } from './tweet/tweet.component'
import { AllusersComponent } from './allusers/allusers.component';

const routes:any=[
  {path:'Login',component:LoginComponent},
  {path:'Register',component:RegistrationComponent},
  {path:'ForgotPassword',component:ForgotpasswordComponent},
  {path:'AllUsers',component:AllusersComponent},
  {path:'**',component:TweetComponent}
];

@NgModule({
  imports:[ RouterModule.forRoot(
    routes,{onSameUrlNavigation:'reload'}
    )],
  exports: [RouterModule]
})
export class AppRoutingModule { }
