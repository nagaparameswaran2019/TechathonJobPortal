import React, { Suspense, useLayoutEffect, useState, useEffect } from "react";
import { Switch, Route } from "react-router-dom";
import { useForm } from "react-hook-form";
import { Grid, GridColumn, GridToolbar } from "@progress/kendo-react-grid";
import { MultiSelect, DropDownList } from "@progress/kendo-react-dropdowns";
import { getOrganizationCoreTypes, saveJobOpening, getJobOpeningsByOrganizationId } from '../../services';
const JobDetails = () => {
    const [lookupDataSource, setLookupDataSource] = useState([]);
    const [employmentType, setEmploymentType] = useState([]);
    const [inputs, setInputs] = useState({});
    const [coreArea, setCoreArea] = useState([]);
    const [employmentTypeValue, setEmploymentTypeValue] = useState([]);
    const [jobDetailsDataSource, setJobDetailsDataSource] = useState([]);

    const { errors, register, handleSubmit } = useForm();

    useEffect(() => {
        setTimeout(() => {
            getOrganizationCoreTypes('COREAREATYPE,ROLETYPE')
                .then((result) => {
                    if (result.isSuccess) {
                        setLookupDataSource(result.data[0].lookUps);
                        setEmploymentType(result.data[1].lookUps);
                    }
                    console.log(lookupDataSource);
                });
            var userData = JSON.parse(localStorage.getItem('userData'));
            // console.log(userData);
            getJobOpeningsByOrganizationId(userData.organizationId)
                .then((result) => {
                    debugger
                    if (result.isSuccess) {

                        console.log(result.data)
                        setJobDetailsDataSource(result.data);
                    }
                });
        }, 100);
    }, []);

    // useLayoutEffect(() => {
    //     window.scrollTo(0, 0);
    // });

    const handleInputChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({ ...values, [name]: value }))
    }

    const handleChange = (event) => {
        // setInputs(values => ({ ...values, ["jobOpeningCoreAreaMapping"]: lookupIds.join() }));
        // setCoreArea(values => ({ ...values, [event.target.name]: event.value }));
        setCoreArea(values => (event.value));
    };

    const empTypeHandleChange = (event) => {
        setEmploymentTypeValue(values => (event.value));
    };

    const handleFormSubmit = (formData) => {

        var lookupIds = [];
        coreArea.forEach((element, index) => {
            lookupIds.push(element.lookUpId);
            console.log(element.lookUpId)
        });
        formData["jobOpeningCoreAreaMapping"] = lookupIds.join();
        formData['employmentTypeId'] = formData.employmentTypeId.lookUpId;

        setTimeout(() => {
            saveJobOpening(formData)
                .then((result) => {
                    if (result.isSuccess) {
                        setInputs({});
                        setCoreArea([]);
                        setEmploymentTypeValue({});
                    }
                    alert(result.message);
                });
        }, 200);

    };

    var jobDetailsGrid = [];
    if (Array.isArray(jobDetailsDataSource) && jobDetailsDataSource.length) {
        jobDetailsGrid = (
            <Grid style={{ height: "400px" }}
                data={jobDetailsDataSource}
                total={jobDetailsDataSource.length} >
                <GridColumn field="numberOfOpening" title="No Of Opening" width="150px" />
                <GridColumn field="minCgpaorPercent" title="Min CGPA/Percent" width="150px" />
                <GridColumn field="jobDescription" title="Job Description" />
                <GridColumn field="jobOpeningCoreAreaMapping" title="Key Skill" />
                <GridColumn field="employmentType" title="Employment Type" width="250px" />
            </Grid>
        );
    } 

    return (
        <div style={{ marginTop: 65 }} className="col-md-12">
            <div className="brand-logo pt-4 pb-4  col-md-6">
                <h6>Job Details</h6>
            </div>
            <div className="col-md-6">
                <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="jobDescription">
                                    Job Description
                                </label>
                                <div className="form-control-wrap">
                                    <textarea  id="jobDescription"
                                        name="jobDescription"
                                        value={inputs.jobDescription || ""}
                                        onChange={handleInputChange}
                                        placeholder="Job Description"
                                        className="form-control-lg form-control"
                                        ref={register({ required: true })}>

                                    </textarea>
                                 {errors.jobDescription && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                                </div>
                            </div>
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="qualification">
                                    Qualification
                                </label>
                                <div className="form-control-wrap">
                                    <input
                                        type="text"
                                        id="qualification"
                                        name="qualification"
                                        value={inputs.qualification || ""}
                                        onChange={handleInputChange}
                                        placeholder="Qualification"
                                        className="form-control-lg form-control"
                                        ref={register({ required: true })}
                                    />
                                      {errors.qualification && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                                </div>
                            </div>
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="role">
                                    Role
                                </label>
                                <div className="form-control-wrap">
                                    <input
                                        type="text"
                                        id="role"
                                        name="role"
                                        value={inputs.role || ""}
                                        onChange={handleInputChange}
                                        placeholder="role"
                                        className="form-control-lg form-control"
                                        ref={register({ required: true })}
                                    />
                                  {errors.role && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                                </div>
                            </div>
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="employmentTypeId">
                                    Employment Type</label>
                                <div className="form-control-wrap">
                                    <DropDownList
                                        data={employmentType}
                                        textField="description"
                                        dataItemKey="lookUpId"
                                        value={employmentTypeValue}
                                        name="employmentTypeId"
                                        onChange={empTypeHandleChange}
                                        defaultItem="Please select ..."
                                        ref={register({ required: false })}
                                    />
                                </div>
                                {errors.employmentTypeId && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                            </div>
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="minCgpaorPercent">
                                    Min Cgpa/Percent
                                </label>
                                <div className="form-control-wrap">
                                    <input
                                        type="text"
                                        id="minCgpaorPercent"
                                        name="minCgpaorPercent"
                                        value={inputs.minCgpaorPercent || ""}
                                        onChange={handleInputChange}
                                        placeholder="Min CGPA/Percent"
                                        ref={register({ required: true })}
                                        className="form-control-lg form-control"
                                    />
                                    {errors.minCgpaorPercent && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                                </div>
                            </div>
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="name">
                                    Core Area
                                </label>
                                <div className="form-control-wrap">
                                    <MultiSelect
                                        data={lookupDataSource}
                                        textField="description"
                                        dataItemKey="lookUpId"
                                        value={coreArea}
                                        name="coreArea"
                                        className=""
                                        onChange={handleChange}
                                        ref={register({ required: true })}
                                        placeholder="Please select"
                                    />
                                    {errors.coreArea && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="col-md-1d2 pt-4">
                        <div className="form-group" style={{ marginBottom: 15 }}>
                            <button
                                type={"submit"}
                                className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-base">
                                Submit
                            </button>
                        </div>
                    </div>
                </form>
            </div>
            <div className="col-md-12">
                {jobDetailsGrid}
            </div>
        </div>
    );
};
export default JobDetails;
