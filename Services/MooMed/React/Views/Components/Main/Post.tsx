import * as React from "react";

import "Views/Components/Main/Styles/PostForm.less";

interface IProps {
    Post: any;
}

interface IState {
    Content: any;
    Posts: any;
}

class Post extends React.Component<IProps> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                {
                    this.props.Post != null ?
                        <p>{this.props.Post.PostContent}</p>
                        :
                        <p>"No Content"</p>
                }
            </div>
        );
    }
}

//Move partial into this and listen to posts inside
class PostFeed extends React.Component<any, IState> {
    constructor(props) {
        super(props);
        this.state = {
            Posts: new Array(),
            Content: new Array(),
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleContentChange = this.handleContentChange.bind(this);
        this.onAjaxSuccess = this.onAjaxSuccess.bind(this);
    }

    componentDidMount() {
        $.ajax({
            url: "Post/GetAllPostsRelatedToAccount",
            method: "POST",
            success: function (result) {
                let posts = JSON.parse(result);
                let postsArray = new Array();

                for (let i = 0; i < posts.length; ++i) {
                    postsArray.push(posts[i]);
                }

                this.setState({
                    Posts: this.state.Posts.concat(postsArray)
                })
            }.bind(this),
            error: function (result) {
                console.log("Failure");
            }
        });
    }

    handleContentChange(e) {
        this.setState({ Content: e.target.value });
    }

    handleSubmit = (e) => {

        e.preventDefault();

        var ajaxSuccessCallback = this.onAjaxSuccess;

        let model = {
            PostContent: this.state.Content
        };

        $.ajax({
            url: "Post/PostStatus",
            method: "POST",
            dataType: "string",
            contentType: "application/json",
            data: {
                model: model
            },
            success: function (result) {
                ajaxSuccessCallback(result);
            },
            error: function (result) {
                console.log("Failure");
            }
        });
    }

    onAjaxSuccess(result) {
        this.setState({
            Posts: this.state.Posts.concat([result.PostModel])
        })
    }
    
    render() {
        return (
            <div>
                <div className="postForm col-6" id="postForm">
                    <div className="postFormSelectionDiv row" id="postFormSelectionDiv">
                        <div className="postFormSelectionButton col-4" id="postFormSelectionNormal">
                            Normal
                        </div>
                            <div className="postFormSelectionButton col-4" id="postFormSelectionMedia">
                                Media
                        </div>
                            <div className="postFormSelectionButton col-4" id="postFormSelectionLink">
                                Link
                        </div>
                    </div>
                    <form onSubmit={this.handleSubmit}>
                        <div className="postFormInput col-10" id="postFormInput">
                            <input className="container-fluid" type="text" value={this.state.Content} onChange={this.handleContentChange} />
                        </div>
                        <div className="postFormSubmitButton col-2 form-group">
                            <input type="submit" className="btn btn-default" value="Post" />
                        </div>
                    </form>
                </div>
                <div>
                    {this.state.Posts.map(function (object, i) {
                        return (
                            <Post key={object.PostId} Post={object}/>
                        );
                    })}
                </div>
            </div>
        );
    }
}