import { FormGroup, FormControl } from '@angular/forms';
import { TweetappService } from './../tweetapp.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-allusers',
  templateUrl: './allusers.component.html',
  styleUrls: ['./allusers.component.css']
})
export class AllusersComponent implements OnInit {

  allUsers:any;
  searchuser=new FormGroup({
    searchinput:new FormControl('')}
  )
  constructor(private service:TweetappService) { }

  ngOnInit(): void {
    this.service.getUserDetails().subscribe((x)=>{
      console.log(x);
      this.allUsers=x;
    })
  }

  searchByUser()
  {
    if(this.searchuser.controls['searchinput'].value=='')
    {
      this.service.getUserDetails().subscribe((x)=>{
        console.log(x);
        this.allUsers=x;
      })
    }
    else{
      this.service.getUserById(this.searchuser.controls['searchinput'].value).subscribe((x:any)=>{
        if(x!=null && x.length>0){
          console.log(x);
          this.allUsers=x;
        }
        else{
          this.allUsers=null;
          alert("user not available");
          
        }
      })
    }
  }

}
