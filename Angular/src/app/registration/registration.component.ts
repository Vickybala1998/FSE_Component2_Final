import { catchError } from 'rxjs';
import { Router } from '@angular/router';
import { TweetappService } from './../tweetapp.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  ispassMatch:boolean=true;
  public users:any;
  message:string='';
  newUser=new FormGroup({
    First_Name:new FormControl('',Validators.required),
    Last_Name:new FormControl('',Validators.required),
    Contact_No:new FormControl('',[Validators.required,Validators.pattern('[0-9]{10}')]),
    Login_Id:new FormControl('',[Validators.required,Validators.pattern('@[A-Za-z0-9]+')]),
    Email:new FormControl('',[Validators.email,Validators.required]),
    Password:new FormControl('',[Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@$!%*?&])[A-Za-z0-9@$!%*?&]{8,16}$')]),
    conPassword:new FormControl('',[Validators.required])
  }, 
  );
  constructor(private service:TweetappService,private router:Router) { }

  ngOnInit(): void {
  } 
  passwordMatch()
  {
    const password=this.newUser.controls['Password'].value;
    const conPassword=this.newUser.controls['conPassword'].value;
    if(password==conPassword)
      return true;
    else
      return false;
  }
  submitForm(){
    if(this.passwordMatch())
    {
      this.ispassMatch=true;
      this.service.registerUser(this.newUser.value).pipe(catchError(this.handleError)).subscribe((x:any)=>{
        if(x.status==200)
        {
         alert('Registration Successfull');
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
      alert('Registration Failed');
      this.router.navigateByUrl('Register');
    }
   
}
