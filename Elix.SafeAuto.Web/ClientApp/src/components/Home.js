import React, { Component } from 'react';
import axios from 'axios';
import Dropzone from 'react-dropzone'
import "bootstrap/dist/css/bootstrap.min.css";

const SERVER_URL = process.env.REACT_APP_ENDPOINT;

export class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedFile: null,
            output: null
        }

    }

    onChangeHandler = files => {
        this.setState({
            selectedFile: files[0],
            loaded: 0
        })
    }

    onClickHandler = () => {
        const data = new FormData();
        data.append('commandFile', this.state.selectedFile);
        axios.post(SERVER_URL, data)
            .then(res => {
                this.setState({ output: res.data })
            })
            .catch(err => {
                this.setState({ output: err.response.data })
            });
    }

    render() {
        let boxText = this.state.selectedFile == null ? "Drag 'n' drop or click to select file" : this.state.selectedFile.name
        return (
            <div>
                <div className="header">
                    <img src={require('../drive.png')} />
                </div>
                <div className="fileUpload">
                    <label>Upload the file with the commands</label>
                    <Dropzone onDrop={this.onChangeHandler} multiple={false}>
                        {({ getRootProps, getInputProps }) => (
                            <section>
                                <div {...getRootProps()}>
                                    <input {...getInputProps()} />
                                    <p>{boxText}</p>
                                </div>
                            </section>
                        )}
                    </Dropzone>
                    <button type="button" className="btn btn-success btn-block" onClick={this.onClickHandler}>Upload</button>
                </div>
                <div>
                    <div>Results</div>
                    <pre>
                        {this.state.output}
                    </pre>
                </div>
            </div>
        );
    }
}
