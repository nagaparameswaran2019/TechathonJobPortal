import React, { Suspense, useLayoutEffect, useState, useEffect } from "react";
import { Switch, Route } from "react-router-dom";
import { useForm } from "react-hook-form";
import { MultiSelect } from "@progress/kendo-react-dropdowns";
import { getOrganizationCoreTypes, saveJobOpening } from '../../services';
const JobDetails = () => {
    const [lookupDataSource, setLookupDataSource] = useState([]);
    const [inputs, setInputs] = useState({});
    const [coreArea, setCoreArea] = useState([]);
    const { errors, register, handleSubmit } = useForm();

    useEffect(() => {
        setTimeout(() => {
            getOrganizationCoreTypes('COREAREATYPE')
                .then((result) => {
                    if (result.isSuccess) {
                        setLookupDataSource(result.data[0].lookUps);
                    }
                    console.log(lookupDataSource);
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

    const handleFormSubmit = (formData) => {
        debugger
        var lookupIds = [];
        coreArea.forEach((element, index) => {
            lookupIds.push(element.lookUpId);
            console.log(element.lookUpId)
        });
        formData["jobOpeningCoreAreaMapping"] = lookupIds.join();
        console.log(formData);

        setTimeout(() => {
            saveJobOpening(formData)
                .then((result) => {
                    debugger
                    if (result.isSuccess) {
                        setInputs({});
                        setCoreArea([]);
                        alert(result.message);
                    }
                    console.log(result);
                });
        }, 200);

    };

    return (
        <div style={{ marginTop: 65, position: "fixed" }} className="col-md-12">
            <div className="brand-logo pt-4 pb-4  col-md-6">
                <h4 tag="h1">Job Details</h4>
            </div>
            <div className="col-md-6">
                <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
                    <div className="row">
                        <div className="col-md-6">
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="firstName">
                                    No Of Job Openings
                                </label>
                                <div className="form-control-wrap">
                                    <input
                                        type="text"
                                        id="numberOfOpening"
                                        name="numberOfOpening"
                                        value={inputs.numberOfOpening || ""}
                                        onChange={handleInputChange}
                                        placeholder="No Of Job Openings"
                                        ref={register({ required: true })}
                                        className="form-control-lg form-control"
                                    />
                                    {errors.name && <p className="invalid">This field is required</p>}
                                </div>
                            </div>
                            <div className="form-group" style={{ marginBottom: 15 }}>
                                <label className="form-label" htmlFor="name">
                                    Min Cgpa/Percent
                                </label>
                                <div className="form-control-wrap">
                                    <input
                                        type="text"
                                        id="minCgpaorPercent"
                                        name="minCgpaorPercent"
                                        value={inputs.minCgpaorPercent || ""}
                                        onChange={handleInputChange}
                                        placeholder="Min Cgpa/Percent"
                                        ref={register({ required: true })}
                                        className="form-control-lg form-control"
                                    />
                                    {errors.name && <p className="invalid">This field is required</p>}
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
                                        placeholder="Please select"
                                    />
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
        </div>
    );
};
export default JobDetails;
