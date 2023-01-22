import React, { Suspense, useLayoutEffect, useState, useEffect } from "react";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import { getOrganizationCoreTypes, addCoreAreasToDepartment, getAllDepartmentByOrganizationId } from '../../services';
import { Switch, Route } from "react-router-dom";
import { useForm } from "react-hook-form";


const DepartmentDetails = () => {
    const [departmentValue, setDepartmentValue] = useState({});
    const [departmentDataSource, setDepartmentDataSource] = useState([]);
    const [lookupDataSource, setLookupDataSource] = useState([]);
    const { errors, register, handleSubmit } = useForm();
    const [coreArea, setCoreArea] = useState([]);
    const [passState, setPassState] = useState(false);

    useEffect(() => {
        setTimeout(() => {
            var organizationId = JSON.parse(localStorage.userData).organizationId;
            getAllDepartmentByOrganizationId(organizationId)
                .then((result) => {
                    debugger
                    if (result.isSuccess) {
                        setDepartmentDataSource(result.data);
                    }
                    console.log(result);
                });

            getOrganizationCoreTypes('COREAREATYPE')
                .then((result) => {
                    if (result.isSuccess) {
                        setLookupDataSource(result.data[0].lookUps);
                    }
                    console.log(lookupDataSource);
                });
        }, 100);
    }, []);

    const handleChange = (event) => {
        setDepartmentValue(event.value);
        setCoreArea([]);
    };
    const onCoreAreaChange = (event) => {
        setPassState(event.value.length > 0);
        setCoreArea(values => (event.value));
    };
    const handleFormSubmit = (dataItem) => {
        console.log(dataItem);
        var lookupIds = [];
        dataItem.coreArea.forEach((element, index) => {
            lookupIds.push(element.lookUpId);
            console.log(element.lookUpId)
        });
        
        var inputData = {  "departmentCoreAreaMappingId": 0,  "departmentId": dataItem.department.departmentId,  "coreAreaTypes": lookupIds.join()}
        
        setTimeout(() => {
            addCoreAreasToDepartment(inputData)
                .then((result) => {
                    debugger
                    if (result.isSuccess) {
                        
                    }
                    setPassState(true);
                    setDepartmentValue({});
                    setCoreArea([]);
                    alert(result.message);
                    console.log(result);
                });
        }, 200);

    };

    useLayoutEffect(() => {
        window.scrollTo(0, 0);
    });
    const defaultItem = {
        name: "Please select", departmentId: -1,
    };

    return (
        <div style={{ marginTop: 75, position: "fixed" }} className="col-md-4">
            <h6>Department Core area mapping</h6>
            <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
                <div className="form-group" style={{ marginBottom: 15 }}>
                    <label className="form-label" htmlFor="name">
                        Department</label>
                    <div className="form-control-wrap">
                        <DropDownList
                            data={departmentDataSource}
                            textField="name"
                            dataItemKey="departmentId"
                            value={departmentValue}
                            name="department"
                            ref={register({ required: true })}
                            // defaultItem={defaultItem}
                            onChange={handleChange}
                        />
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
                            ref={register({ required: true })}
                            onChange={onCoreAreaChange}
                            placeholder="Please select"
                        />
                    </div>
                </div>
                <div className="k-form-buttons">
                    <button
                        type={"submit"}
                        disabled={!passState}
                        className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-base">
                        Submit
                    </button>
                </div>

            </form>
        </div>
    );
};
export default DepartmentDetails;
