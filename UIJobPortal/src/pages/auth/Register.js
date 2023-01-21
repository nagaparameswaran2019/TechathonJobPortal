import React, { useState, useEffect } from "react";
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
import { Spinner, FormGroup, Form } from "reactstrap";
import PageContainer from "../../layout/page-container/PageContainer";
import Head from "../../layout/head/Head";
import AuthFooter from "./AuthFooter";
import { useForm } from "react-hook-form";
import { Link } from "react-router-dom";
import { ComboBox, MultiSelect } from "@progress/kendo-react-dropdowns";
import { getOrganizationCoreTypes } from '../../services';
import { de } from "date-fns/locale";

// import {
//   MultiSelect,
//   MultiSelectChangeEvent,
// } from "@progress/kendo-react-dropdowns";

const Register = ({ history }) => {
  var coreTypeDataSource = [{ CoreTypeID: 1, CoreType: "MCA" },
  { CoreTypeID: 2, CoreType: "MBA" }, { CoreTypeID: 3, CoreType: "BCA" }];

  const [inputs, setInputs] = useState({});
  const [passState, setPassState] = useState(false);
  const [showDepartment, setshowDepartment] = useState(true);
  const [loading, setLoading] = useState(false);
  const [dataSource, setdataSource] = useState([]);
  const [lookupDataSource, setlookupDataSource] = useState([]);
  const [departmentLookup, setDepartmentLookup] = useState([]);
  const { errors, register, handleSubmit } = useForm();
  const [value, setValue] = React.useState(null);
  const [department, setDepartment] = React.useState(null);
  

  useEffect(() => {
    setTimeout(() => {
      getOrganizationCoreTypes('COMPANYTYPE,INSTITUTIONTYPE,DEPARTMENTTYPE')
        .then((result) => {
          if (result.isSuccess) {
            setlookupDataSource(result.data);
            var lookup = result.data.find(s => s.code == "INSTITUTIONTYPE").lookUps;
            setdataSource(lookup);
            lookup = result.data.find(s => s.code == "DEPARTMENTTYPE").lookUps;
            setDepartmentLookup(lookup);
          }
          console.log(result);
        });
    }, 500);
  }, []);

  const handleFormSubmit = () => {
    console.log(inputs);
    // setLoading(true);
    // setTimeout(() => history.push(`${process.env.PUBLIC_URL}/auth-success`), 2000);
  };
 
  const onChangeDepartment = (event)=>{ 
    var lookupIds = [];
    event.value.forEach((element, index) => {
      lookupIds.push(element.lookUpId);
      console.log(element.lookUpId)
    });
     setDepartment(event.value);
  };
  const handleChange = (event) => {
    setDepartment([]);
    setValue(event.value);
  };
  const sizes = ["X-Small", "Small", "Medium", "Large", "X-Large", "2X-Large"];

  const handleInputChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setInputs(values => ({ ...values, [name]: value }))
  }
  const onRadioButtonChanged = (event) => {
    setValue("");
    setDepartment([]);
    var orgType = event.currentTarget.dataset.orgType;
    setshowDepartment(orgType == 'INSTITUTIONTYPE' ? true : false);
    var result = lookupDataSource.find(s => s.code == event.currentTarget.dataset.orgType).lookUps;
    setdataSource(result);
  };

  return (
    <React.Fragment>
      <Head title="Register" />
      <PageContainer>
        <Block className="nk-block-middle nk-auth-body wide-xs  ">
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
                    value={inputs.name || ""}
                    onChange={handleInputChange}
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
                    value={inputs.email || ""}
                    ref={register({ required: true })}
                    onChange={handleInputChange}
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
                        data-org-type="INSTITUTIONTYPE"
                        onChange={onRadioButtonChanged} />
                      <label className="custom-control-label" htmlFor="radio-bl">Institution</label>
                    </div>
                    <div className="custom-control custom-control-sm custom-radio" style={{ marginLeft: 10 }}>
                      <input type="radio"
                        className="custom-control-input"
                        data-org-type="COMPANYTYPE"
                        name="radio-list"
                        id="radio-ph"
                        onChange={onRadioButtonChanged} />
                      <label className="custom-control-label" htmlFor="radio-ph">Company</label>
                    </div>
                  </div>
                </FormGroup>
              {/* style={{ marginLeft: 10, maxWidth: calc(100 % - 10), marginRight: 10 }} */}
              <fieldset >
                <legend>Organization Info</legend>
                <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Last Name</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"lastName"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Email</label>
                            <div class="form-control-wrap">
                                <input
                                    name={"email"}
                                    type={"email"}
                                    component={EmailInput}
                                    validator={emailValidator}
                                />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Contact</label>
                            <div class="form-control-wrap">
                               
                            </div>
                        </div>
            
                <FormGroup>
                  <div className="form-label-group">
                    <label className="form-label" htmlFor="default-033">
                      Organization Core Type
                    </label>
                  </div>
                  <div className="form-control-wrap">
                    <ComboBox
                      data={dataSource}
                      textField="description"
                      dataItemKey="lookUpId"
                      value={value}
                      name="OrganizationCoreType"
                      onChange={handleChange}
                      placeholder="Please select ..." 
                    />
                  </div>
                </FormGroup>
                <FormGroup className={showDepartment ? 'd-block' : 'd-none'}>
                  <div className="form-label-group">
                    <label className="form-label" htmlFor="default-033">
                      Department Type
                    </label>
                  </div>
                  <div className="form-control-wrap">
                    <MultiSelect
                      data={departmentLookup}
                      textField="description"
                      dataItemKey="lookUpId"
                      value={department}
                      name="department"
                      onChange={onChangeDepartment} 
                      placeholder="Please select ..."
                    />
                  </div>
                </FormGroup>
              </fieldset>

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
                    name="password"
                    value={inputs.password || ""}
                    onChange={handleInputChange}
                    ref={register({ required: "This field is required" })}
                    placeholder="Enter your password"
                    className={`form-control-lg form-control ${passState ? "is-hidden" : "is-shown"}`}
                  />
                  {errors.passcode && <span className="invalid">{errors.passcode.message}</span>}
                </div>
              </FormGroup>
              <FormGroup>
                <div className="form-label-group">
                  <label className="form-label" htmlFor="confirmpassword">
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
                    name="confirmpassword"
                    value={inputs.confirmpassword || ""}
                    onChange={handleInputChange}
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
