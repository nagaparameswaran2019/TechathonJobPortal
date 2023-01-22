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
import { Link, useHistory   } from "react-router-dom";
import { ComboBox, MultiSelect } from "@progress/kendo-react-dropdowns";
import { getOrganizationCoreTypes, registerUserDetails } from '../../services';
import { de } from "date-fns/locale";
import { NavigatorFilterEvent } from "@progress/kendo-react-charts"; 

const Register = ({ history }) => {
  var _inputs = {
    "userId": 0,
    "firstName": "",
    "lastName": "",
    "userName": "",
    "password": "",
    "createdDate": "2023-01-21T05:50:23.534Z",
    "modifiedDate": "2023-01-21T05:50:23.534Z",
    "organizationId": 0
  };


  const [inputs, setInputs] = useState(_inputs);

  const [orgInputs, setOrgInputs] = useState({
    "organizationId": 0,
    "name": "",
    "email": "",
    "website": "",
    "contact": "",
    "organizationTypeId": 11,
    "organizationSubTypeId": 0,
    "department": "",
    "departmentdata": []
  });
  const [passState, setPassState] = useState(false);
  const [showDepartment, setshowDepartment] = useState(true);
  const [loading, setLoading] = useState(false);
  const [dataSource, setdataSource] = useState([]);
  const [lookupDataSource, setlookupDataSource] = useState([]);
  const [departmentLookup, setDepartmentLookup] = useState([]);
  const { errors, register, handleSubmit } = useForm();
  const [value, setValue] = React.useState(null);
  const [department, setDepartment] = React.useState(null);
  const [orgType, setOrgType] = React.useState(null);
  const [orgSubType, setOrgSubType] = React.useState(null);
  const history1 = useHistory();

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
    }, 300);
  }, []);

  const handleFormSubmit = () => {
     
    var inputData = inputs;
    inputData['organization'] = orgInputs;
    var lookupIds = [];
    inputData.organization.departmentdata.forEach((element, index) => {
      lookupIds.push(element.lookUpId);
      console.log(element.lookUpId)
    });
    inputData.organization.organizationSubTypeId = orgSubType.lookUpId;

    if(orgType == 'INSTITUTIONTYPE'){
      inputData.organization.department = lookupIds.join();
    } 
    
    setTimeout(() => {
      registerUserDetails(inputData)
        .then((result) => {
          debugger
          if (result.isSuccess) {
            history.push(`${process.env.PUBLIC_URL}/auth-login`);
          }
          console.log(result);
        });
    }, 200);
  };

  const onChangeDepartment = (event) => { 
    setOrgInputs(values => ({ ...values, [event.target.name]: event.value }))
  };

  const handleChange = (event) => { 
    setDepartment([]); 
    setOrgSubType(event.value); 
  };

  const handleInputChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setInputs(values => ({ ...values, [name]: value }))
  }

  const handleOrgInputChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setOrgInputs(values => ({ ...values, [name]: value }))
  }

  const onRadioButtonChanged = (event) => {
    setValue("");
    
    setDepartment([]);
    var orgType = event.currentTarget.dataset.orgType;
    setshowDepartment(orgType == 'INSTITUTIONTYPE' ? true : false);
    setOrgType(orgType);

    var orgTypeID = orgType == 'INSTITUTIONTYPE'? 11:10;
    setOrgInputs(values => ({ ...values, ['organizationTypeId']: orgTypeID }))
    
    var result = lookupDataSource.find(s => s.code == event.currentTarget.dataset.orgType).lookUps;
    setdataSource(result);
    setOrgSubType({});
    setOrgInputs(values => ({ ...values, ['departmentdata']: {} }))
  };

  return (
    <React.Fragment>
      <Head title="Register" />
      <PageContainer>
        <Block className="nk-block-middle nk-auth-body">
          <div className="brand-logo pb-4 text-center">
            <h2 tag="h4">Job Portal</h2>
          </div>
          <PreviewCard className="card-bordered" bodyClass="card-inner-lg">
            <BlockHead>
              <BlockContent className="text-center">
                <h4>Register</h4>
              </BlockContent>
            </BlockHead>
            <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
              <div className="row">
                <div className="col-md-6">
                  <FormGroup>
                    <label className="form-label" htmlFor="firstName">
                      First Name
                    </label>
                    <div className="form-control-wrap">
                      <input
                        type="text"
                        id="firstName"
                        name="firstName"
                        value={inputs.firstName || ""}
                        onChange={handleInputChange}
                        placeholder="Enter your name"
                        ref={register({ required: true })}
                        className="form-control-lg form-control"
                      />
                      {errors.name && <p className="invalid">This field is required</p>}
                    </div>
                  </FormGroup>
                  <FormGroup>
                    <label className="form-label" htmlFor="name">
                      Last Name
                    </label>
                    <div className="form-control-wrap">
                      <input
                        type="text"
                        id="lastname"
                        name="lastname"
                        value={inputs.lastname || ""}
                        onChange={handleInputChange}
                        placeholder="Enter your last name"
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
                        name="userName"
                        value={inputs.userName || ""}
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
                        href="#confirmPassword"
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
                </div>
                <div className="col-md-6" style={{ borderLeft: "1px solid #dbdfea" }}>
                  <div className="form-group">
                    <label className="form-label" htmlFor="organization.name">
                      Name</label>
                    <div className="form-control-wrap">
                      <input
                        type="text"
                        id="organization_name"
                        name="name"
                        value={orgInputs.name || ""}
                        onChange={handleOrgInputChange}
                        placeholder="Enter organization name"
                        ref={register({ required: true })}
                        className="form-control-lg form-control"
                      />
                    </div>
                  </div>
                  <div className="form-group">
                    <label className="form-label" htmlFor="organization.email">
                      Email</label>
                    <div className="form-control-wrap">
                      <input
                        name="email"
                        id="organization_email"
                        type="text"
                        value={orgInputs.email || ""}
                        onChange={handleOrgInputChange}
                        className="form-control-lg form-control" />
                    </div>
                  </div>
                  <div className="form-group">
                    <label className="form-label" htmlFor="contact">
                      Contact</label>
                    <div className="form-control-wrap">
                      <input
                        type="text"
                        id="organization_contact"
                        name="contact"
                        value={orgInputs.contact || ""}
                        onChange={handleOrgInputChange}
                        placeholder="Enter organization contact"
                        ref={register({ required: true })}
                        className="form-control-lg form-control"
                      />
                    </div>
                  </div>
                  <div className="form-group">
                    <label className="form-label" htmlFor="organization.website">
                      Website</label>
                    <div className="form-control-wrap">
                      <input
                        type="text"
                        id="organization_website"
                        name="website"
                        value={orgInputs.website || ""}
                        onChange={handleOrgInputChange}
                        placeholder="Enter organization contact"
                        ref={register({ required: true })}
                        className="form-control-lg form-control"
                      />
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
                        value={orgSubType}
                        name="orgSubType"
                        onChange={handleChange}
                        placeholder="Please select"
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
                        value={orgInputs.departmentdata}
                        name="departmentdata"
                        onChange={onChangeDepartment}
                        placeholder="Please select"
                      />
                    </div>
                  </FormGroup>
                </div>
              </div>
              <div className="col-md-1d2 pt-4">
                <FormGroup>
                  <Button type="submit" color="primary" size="lg" className="btn-block">
                    {loading ? <Spinner size="sm" color="light" /> : "Register"}
                  </Button>
                </FormGroup>
              </div>
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
