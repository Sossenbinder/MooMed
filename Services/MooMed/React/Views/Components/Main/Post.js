"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
require("Views/Components/Main/Styles/PostForm.less");
class Post extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (React.createElement("div", null, this.props.Post != null ?
            React.createElement("p", null, this.props.Post.PostContent)
            :
                React.createElement("p", null, "\"No Content\"")));
    }
}
class PostFeed extends React.Component {
    constructor(props) {
        super(props);
        this.handleSubmit = (e) => {
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
        };
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
                });
            }.bind(this),
            error: function (result) {
                console.log("Failure");
            }
        });
    }
    handleContentChange(e) {
        this.setState({ Content: e.target.value });
    }
    onAjaxSuccess(result) {
        this.setState({
            Posts: this.state.Posts.concat([result.PostModel])
        });
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("div", { className: "postForm col-6", id: "postForm" },
                React.createElement("div", { className: "postFormSelectionDiv row", id: "postFormSelectionDiv" },
                    React.createElement("div", { className: "postFormSelectionButton col-4", id: "postFormSelectionNormal" }, "Normal"),
                    React.createElement("div", { className: "postFormSelectionButton col-4", id: "postFormSelectionMedia" }, "Media"),
                    React.createElement("div", { className: "postFormSelectionButton col-4", id: "postFormSelectionLink" }, "Link")),
                React.createElement("form", { onSubmit: this.handleSubmit },
                    React.createElement("div", { className: "postFormInput col-10", id: "postFormInput" },
                        React.createElement("input", { className: "container-fluid", type: "text", value: this.state.Content, onChange: this.handleContentChange })),
                    React.createElement("div", { className: "postFormSubmitButton col-2 form-group" },
                        React.createElement("input", { type: "submit", className: "btn btn-default", value: "Post" })))),
            React.createElement("div", null, this.state.Posts.map(function (object, i) {
                return (React.createElement(Post, { key: object.PostId, Post: object }));
            }))));
    }
}
//# sourceMappingURL=Post.js.map