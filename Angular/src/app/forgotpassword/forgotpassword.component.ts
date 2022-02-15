import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import{TweetappService} from '.././tweetapp.service';
import { catchError } from 'rxjs';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent implements OnInit {

  userName:string='';
  ispassMatch:boolean=true;
  forgotpassword=new FormGroup({
    userName:new FormControl('',Validators.required),
    Password:new FormControl('',[Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&])[A-Za-z0-9@$!%*?&]{8,16}$')]),
    ConPassword:new FormControl('',Validators.required)

  });
  constructor(private service:TweetappService,private router:Router) { }

  ngOnInit(): void {
  }

  forgotpass()
  {
    if(this.passwordMatch())
    {
      this.ispassMatch=true;
      this.userName=this.forgotpassword.controls['userName'].value;
      this.service.forgotPassword(this.forgotpassword.value,this.userName).pipe(catchError(this.handleError)).subscribe(
       (x:any)=>{
         if(x.status==200){
           alert("passwors is reset");
           this.router.navigateByUrl('Login');
         }
       }
      )
    }
    else
    {
      this.ispassMatch=false;
    }
    
  }

handleError():any
{
  alert('unable to reset password.please try agian after sometime ');
}
passwordMatch()
{
  const password=this.forgotpassword.controls['Password'].value;
  const conPassword=this.forgotpassword.controls['ConPassword'].value;
  if(password==conPassword)
    return true;
  else
    return false;
}
}
