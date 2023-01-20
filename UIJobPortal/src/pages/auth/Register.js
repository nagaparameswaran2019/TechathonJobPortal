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
import { Spinner, FormGroup, Form } from "reactstrap";
import PageContainer from "../../layout/page-container/PageContainer";
import Head from "../../layout/head/Head";
import AuthFooter from "./AuthFooter";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { ComboBox } from "@progress/kendo-react-dropdowns";

const Register = ({ history }) => {
  var coreTypeDataSource = [{ CoreTypeID: 1, CoreType: "MCA" },
  { CoreTypeID: 2, CoreType: "MBA" }, { CoreTypeID: 3, CoreType: "BCA" }];

  const [passState, setPassState] = useState(false);
  const [loading, setLoading] = useState(false);
  const [dataSource, setdataSource] = useState(coreTypeDataSource);
  const { errors, register, handleSubmit } = useForm();

  const [value, setValue] = React.useState(null);

  const handleFormSubmit = () => {
    setLoading(true);
    setTimeout(() => history.push(`${process.env.PUBLIC_URL}/auth-success`), 2000);
  };

  const handleChange = (event) => {
    setValue(event.value);
  };
  
  const onRadioButtonChanged = (event) => {
    setValue("");
    if (event.currentTarget.dataset.orgType == "Institution") {
      setdataSource(coreTypeDataSource);
    }
    else {
      var coreTypeDataSource1 = [{ CoreTypeID: 1, CoreType: "IT" },
      { CoreTypeID: 2, CoreType: "ITES" }, { CoreTypeID: 3, CoreType: "Civil" }];
      setdataSource(coreTypeDataSource1);
    } 
  };

  return (
    <React.Fragment>
      <Head title="Register" />
      <PageContainer>
        <Block className="nk-block-middle nk-auth-body  wide-xs">
          <div className="brand-logo pb-4 text-center">
            <h2 tag="h4">Job Portal</h2>
          </div>
          <PreviewCard className="card-bordered" bodyClass="card-inner-lg">
            <BlockHead>
              <BlockContent>
                <h4>Register</h4>
              </BlockContent>
            </BlockHead>
            <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
              <FormGroup>
                <label className="form-label" htmlFor="name">
                  Name
                </label>
                <div className="form-control-wrap">
                  <input
                    type="text"
                    id="name"
                    name="name"
                    placeholder="Enter your name"
                    ref={register({ required: true })}
                    className="form-control-lg form-control"
                  />
                  {errors.name && <p className="invalid">This field is required</p>}
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="default-01">
                    Email or Username
                  </label>
                </div>
                <div className="form-control-wrap">
                  <input
                    type="text"
                    bssize="lg"
                    id="default-01"
                    name="email"
                    ref={register({ required: true })}
                    className="form-control-lg form-control"
                    placeholder="Enter your email address or username"
                  />
                  {errors.email && <p className="invalid">This field is required</p>}
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="default-01">
                    Organization Type
                  </label>
                </div>
                <div className="form-control-wrap">
                  <div className="custom-control custom-control-sm custom-radio">
                    <input type="radio" className="custom-control-input"
                      defaultChecked={true}
                      name="radio-list"
                      id="radio-bl"
                      data-org-type="Institution"
                      onChange={onRadioButtonChanged} />
                    <label className="custom-control-label" htmlFor="radio-bl">Institution</label>
                  </div>
                  <div className="custom-control custom-control-sm custom-radio" style={{ marginLeft: 10 }}>
                    <input type="radio" className="custom-control-input" data-org-type="Company" name="radio-list" id="radio-ph" onChange={onRadioButtonChanged} />
                    <label className="custom-control-label" htmlFor="radio-ph">Company</label>
                  </div>
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="default-033">
                    Organization Core Type
                  </label>
                </div>
                <div className="form-control-wrap">
                  <ComboBox
                    data={dataSource}
                    textField="CoreType"
                    dataItemKey="CoreTypeID"
                    value={value}
                    onChange={handleChange}
                    placeholder="Please select ..."
                    style={{
                      width: "300px",
                    }}
                  />
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="password">
                    Password
                  </label>
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
                    name="passcode"
                    ref={register({ required: "This field is required" })}
                    placeholder="Enter your password"
                    className={`form-control-lg form-control ${passState ? "is-hidden" : "is-shown"}`}
                  />
                  {errors.passcode && <span className="invalid">{errors.passcode.message}</span>}
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="password">
                    Confirm Password
                  </label>
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
                    id="confirmPassword"
                    name="confirmPassword"
                    ref={register({ required: "This field is required" })}
                    placeholder="Enter your confirm password"
                    className={`form-control-lg form-control ${passState ? "is-hidden" : "is-shown"}`}
                  />
                  {errors.passcode && <span className="invalid">{errors.passcode.message}</span>}
                </div>
              </FormGroup>
              <FormGroup>
                <Button type="submit" color="primary" size="lg" className="btn-block">
                  {loading ? <Spinner size="sm" color="light" /> : "Register"}
                </Button>
              </FormGroup>
            </form>
            <div className="form-note-s2 text-center pt-4">
              {" "}
              Already have an account?{" "}
              <Link to={`${process.env.PUBLIC_URL}/auth-login`}>
                <strong>Sign in instead</strong>
              </Link>
            </div>
            <div className="text-center pt-4 pb-3">
              <h6 className="overline-title overline-title-sap">
                <span>OR</span>
              </h6>
            </div>
            <ul className="nav justify-center gx-8">
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
export default Register;
