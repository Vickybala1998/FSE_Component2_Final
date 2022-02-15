import { Router } from '@angular/router';
import { TweetappService } from './../tweetapp.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, tap } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isLoggedIn:string='';
  constructor(private service:TweetappService,private router:Router) { }

  ngOnInit(): void {
  }
  loginuser=new FormGroup({
    Login_id:new FormControl('',Validators.required),
    Password:new FormControl('',Validators.required)
  });

  loginForm()
  {
    let user_id=this.loginuser.value["Login_id"];
    console.log(this.loginuser);
    this.service.login(this.loginuser.value).pipe(catchError(this.handleError)).subscribe
    ((x:any)=>
      {
        console.log(x.status);
        if(x.status==200)
        {
          this.isLoggedIn="true";
          localStorage.setItem('loggedStatus',this.isLoggedIn)
          localStorage.setItem('userId',user_id);
          alert('login successfull');
          this.router.navigateByUrl('home').then(()=>{
            window.location.reload();
          });
        }
      })
}
handleError():any
{
  alert('invalid login details');
  this.router.navigateByUrl('login');
}
}
