import React, { useState } from "react";
import {
  Block,
  BlockContent,
  BlockDes,
  BlockHead,
  BlockTitle,
  Button,
  Icon,
  PreviewCard,
} from "../../components/Component";
import Logo from "../../images/logo.png";
import LogoDark from "../../images/logo-dark.png";
import { Form, FormGroup, Spinner, Alert } from "reactstrap";
import PageContainer from "../../layout/page-container/PageContainer";
import Head from "../../layout/head/Head";
import AuthFooter from "./AuthFooter";
import { useForm } from "react-hook-form";
import { Link, useHistory } from "react-router-dom";
import { userLogin } from "../../services";


const Login = ({ history }) => {
  const [loading, setLoading] = useState(false);
  const [passState, setPassState] = useState(false);
  const [inputData, setInputData] = useState({});
  const [errorVal, setError] = useState("");

  const onFormSubmit = (formData) => {
    setLoading(true);
    localStorage.setItem("accessToken", "token");
    setTimeout(() => {

      //sessionStorage.setItem('LoginOrgType', 'INSTITUTION'); 
      sessionStorage.setItem('LoginOrgType', 'COMPANY');
      var _inputData = inputData;
      // return
      userLogin(formData)
        .then((result) => {
          debugger
          if (result.isSuccess) {
            localStorage.setItem("accessToken", result);
            localStorage.setItem("organizationId", result.data.organizationId);
            localStorage.setItem("userData", JSON.stringify(result.data));

            window.history.pushState(
              `${process.env.PUBLIC_URL ? process.env.PUBLIC_URL : "/"}`,
              "auth-login",
              "pages/app/Dashboard",
              `${process.env.PUBLIC_URL ? process.env.PUBLIC_URL : "/"}`
            );
          }
          console.log(result);
          window.location.reload();
        });
    }, 300);
    // } 
    // else {
    //   setTimeout(() => {
    //     setError("Cannot login with credentials");
    //     setLoading(false);
    //   }, 2000);
    // }
  };

  const handleInputChange = (event) => {

    const name = event.target.name;
    const value = event.target.value;
    setInputData(values => ({ ...values, [name]: value }))
  }
  const { errors, register, handleSubmit } = useForm();

  return (
    <React.Fragment>
      <Head title="Login" />
      <PageContainer>
        <Block className="nk-block-middle nk-auth-body  wide-xs">
          <PreviewCard className="card-bordered" bodyClass="card-inner-lg">
            <BlockHead>
              <BlockContent>
                <div className="pb-4 text-center">
                  <h4>Campus Recruitment Platform(CRP)</h4>
                </div>
              </BlockContent>
              <BlockContent>
                <BlockTitle tag="h4">Sign-In</BlockTitle>
              </BlockContent>
            </BlockHead>
            {errorVal && (
              <div className="mb-3">
                <Alert color="danger" className="alert-icon">
                  {" "}
                  <Icon name="alert-circle" /> Unable to login with credentials{" "}
                </Alert>
              </div>
            )}
            <Form className="is-alter" onSubmit={handleSubmit(onFormSubmit)}>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="default-01">
                    Email or Username
                  </label>
                </div>
                <div className="form-control-wrap">
                  <input
                    type="text"
                    id="default-01"
                    name="UserName"
                    ref={register({ required: "This field is required" })}
                    defaultValue=""
                    value={inputData.UserName || ""}
                    onChange={handleInputChange}
                    placeholder="Enter your email address or username"
                    className="form-control-lg form-control"
                  />
                  {errors.UserName && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="password">
                    Password
                  </label>
                  <Link className="link link-primary link-sm" to={`${process.env.PUBLIC_URL}/auth-reset`}>
                    Forgot Code?
                  </Link>
                </div>
                <div className="form-control-wrap">
                  <a
                    href="#password"
                    onClick={(ev) => {
                      ev.preventDefault();
                      setPassState(!passState);
                    }}
                    className={`form-icon lg form-icon-right passcode-switch ${passState ? "is-hidden" : "is-shown"}`}
                  >
                    <Icon name="eye" className="passcode-icon icon-show"></Icon>

                    <Icon name="eye-off" className="passcode-icon icon-hide"></Icon>
                  </a>
                  <input
                    type={passState ? "text" : "password"}
                    id="password"
                    name="Password"
                    defaultValue=""
                    value={inputData.Password || ""}
                    onChange={handleInputChange}
                    ref={register({ required: "This field is required" })}
                    placeholder="Enter your password"
                    className={`form-control-lg form-control ${passState ? "is-hidden" : "is-shown"}`}
                  />
                {errors.Password && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                </div>
              </FormGroup>
              <FormGroup>
                <Button size="lg" className="btn-block" type="submit" color="primary">
                  {loading ? <Spinner size="sm" color="light" /> : "Sign in"}
                </Button>
              </FormGroup>
            </Form>
            <div className="form-note-s2 text-center pt-4">
              {" "}
              New on our platform? <Link to={`${process.env.PUBLIC_URL}/auth-register`}>Create an account</Link>
            </div>
            <div className="text-center pt-4 pb-3">
              <h6 className="overline-title overline-title-sap">
                <span>OR</span>
              </h6>
            </div>
            <ul className="nav justify-center gx-4">
              <li className="nav-item">
                <a
                  className="nav-link"
                  href="#socials"
                  onClick={(ev) => {
                    ev.preventDefault();
                  }}
                >
                  Facebook
                </a>
              </li>
              <li className="nav-item">
                <a
                  className="nav-link"
                  href="#socials"
                  onClick={(ev) => {
                    ev.preventDefault();
                  }}
                >
                  Google
                </a>
              </li>
            </ul>
          </PreviewCard>
        </Block>
        <AuthFooter />
      </PageContainer>
    </React.Fragment>
  );
};
export default Login;
