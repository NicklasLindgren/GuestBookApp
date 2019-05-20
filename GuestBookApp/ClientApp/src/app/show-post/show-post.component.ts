import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Post } from '../models/post.model';

@Component({
  selector: 'app-show-post',
  templateUrl: './show-post.component.html'
})
export class ShowPostComponent {
  public posts: Post[];
  public post: Post;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<Post[]>(baseUrl + 'api/posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }

  getPosts() {
    this.http.get<Post[]>(this.baseUrl + 'api/posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }

  addPost(message: String) {
    if (message !== '') {
      return this.http.post('/api/posts', {
        message: message
      }).subscribe(result => {
        this.getPosts();
      }, error => console.error(error));
    } else {
      alert('Please Enter a Message!');
    }
   
  }

  likePost(id: String) {
   
    return this.http.put('/api/posts', {
      id: id.id
    }).subscribe(result => {
      this.getPosts();
    }, error => console.error(error));
    

  }


}
