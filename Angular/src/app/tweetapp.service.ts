import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import{Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TweetappService {
  private users=[];
  private _refresh=new Subject<void>();
  constructor(private http:HttpClient) { }

  getRefresh()
  {
    return this._refresh;
  }
  getUserDetails()
  {
    return this.http.get('http://localhost:39975/api/v1.0/tweets/users/all');
  }

  registerUser(form:any)
  {
    return this.http.post("http://localhost:39975/api/v1.0/tweets/register",form,{observe: 'response'});
  }

  getTweets()
  {
    return this.http.get("http://localhost:39975/api/v1.0/tweets/all");
  }

  deleteTweet(username:any,id:any)
  {
    return this.http.delete("http://localhost:39975/api/v1.0/tweets/"+username+"/delete/"+id,{observe: 'response'}).subscribe(()=>
    {
      this._refresh.next();
    });
  }

  postTweet(form:any,username:any):any
  {
    return this.http.post("http://localhost:39975/api/v1.0/tweets/"+username+"/add",form,{observe: 'response'}).subscribe((x)=>
    {
      this._refresh.next();
      return x;
    })
  }

  login(form:any)
  {
    return this.http.post("http://localhost:39975/api/v1.0/tweets/login",form,{observe: 'response'})
  }
  updateTweet(form:any,username:any,id:any):any
  {
    return this.http.put("http://localhost:39975/api/v1.0/tweets/"+username+"/update/"+id,form,{observe: 'response'}).subscribe((x)=>
    {
      this._refresh.next();
      return x;
    })
  }
  forgotPassword(form:any,username:any)
  {
    return this.http.put("http://localhost:39975/api/v1.0/tweets/"+username+"/forgot",form,{observe: 'response'});
  }
  replyTweet(form:any,id:any,username:any)
  {
    return this.http.put("http://localhost:39975/api/v1.0/tweets/"+username+"/reply/"+id,form,{observe: 'response'}).subscribe(_=>{
      this._refresh.next();
    });
  }
  likeTweet(id:any,username:any,like:number)
  {
    return this.http.put("http://localhost:39975/api/v1.0/tweets/"+username+"/like/"+id+"?like="+like,{observe: 'response'}).subscribe(_=>{
      this._refresh.next();
    });
  }
  getTweetByUserId(username:string)
  {
      return this.http.get("http://localhost:39975/api/v1.0/tweets/"+username);
  }
  getUserById(username:string)
  {
    return this.http.get("http://localhost:39975/api/v1.0/tweets/user/search/"+username)
  }
}
