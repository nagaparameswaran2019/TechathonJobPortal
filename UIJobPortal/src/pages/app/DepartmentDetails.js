import React, { Suspense, useLayoutEffect, useState, useEffect } from "react";
import { DropDownList, MultiSelect } from "@progress/kendo-react-dropdowns";
import { getOrganizationCoreTypes, addCoreAreasToDepartment, getAllDepartmentByOrganizationId } from '../../services';
import { Switch, Route } from "react-router-dom";
import { Grid, GridColumn, GridToolbar } from "@progress/kendo-react-grid";
import { useForm } from "react-hook-form";


const DepartmentDetails = () => {
    const [departmentValue, setDepartmentValue] = useState({});
    const [departmentDataSource, setDepartmentDataSource] = useState([]);
    const [lookupDataSource, setLookupDataSource] = useState([]);
    const { errors, register, handleSubmit } = useForm();
    const [coreArea, setCoreArea] = useState([]);
    const [passState, setPassState] = useState(false);
    const [gridData, setGridData] = useState([]);
 
    useEffect(() => {
        setTimeout(() => {
            var organizationId = JSON.parse(localStorage.userData).organizationId;
            getAllDepartmentByOrganizationId(organizationId)
                .then((result) => {

                    if (result.isSuccess) {
                        setDepartmentDataSource(result.data);
                        setGridData(result.data);
                    }
                    
                    getOrganizationCoreTypes('COREAREATYPE')
                        .then((output) => {
                            if (output.isSuccess) {
                                setLookupDataSource(output.data[0].lookUps);
                            }
                            console.log(lookupDataSource);
                            result.data.forEach(r => {

                                r['coreAreaLookup'] = [];
                                r['selectedCoreAreas'] = '';

                                if (r.departmentCoreAreaMapping !== '') {
                                    var coreAreaSelected = [];
                                    var _coreAreas = r.departmentCoreAreaMapping.split(',');
                                    _coreAreas.forEach((c, index) => {
                                        debugger
                                        var lookup = output.data[0].lookUps.find(s => s.lookUpId == c);
                                        r.coreAreaLookup.push(lookup);
                                        coreAreaSelected.push(lookup.description);
                                        console.log(lookup); 
                                    })
                                    r.selectedCoreAreas = coreAreaSelected.join(', ');
                                } 
                                departmentDataSource.push(r); 
                            }) 
                        }); 
                }); 
        }, 500);
    }, []);

    const handleChange = (event) => {
        debugger
        setDepartmentValue(event.value);
        setCoreArea(event.value.coreAreaLookup);
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

        var inputData = { "departmentCoreAreaMappingId": 0, "departmentId": dataItem.department.departmentId, "coreAreaTypes": lookupIds.join() }

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

    var departmentDetailsGrid = [];
    if (Array.isArray(gridData) && gridData.length) {
        departmentDetailsGrid = (
            <Grid style={{ height: "400px" }}
                data={gridData}
                total={gridData.length} >
                <GridColumn field="name" title="Name" width="400px" />
                <GridColumn field="selectedCoreAreas" title="Core Area's" /> 
                <GridColumn field="" title="Action" width="250px" />
            </Grid>
        );
    }

    return (
        <div style={{ marginTop: 75 }} className="col-md-12">
            <h6>Department Core area mapping</h6>
            <form className="is-alter" onSubmit={handleSubmit(handleFormSubmit)}>
                <div className="col-md-3">
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
                                onChange={handleChange}
                            />
                             {errors.department && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
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
                            {errors.coreArea && <div role="alert" className="k-form-error k-text-start">This field is required</div>}
                        </div>
                    </div>
                    <div className="k-form-buttons pb-4">
                        <button
                            type={"submit"}
                            disabled={!passState}
                            className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-base">
                            Submit
                        </button>
                    </div>
                </div>
            </form>
            <div className="col-md-12">
                {departmentDetailsGrid}
            </div>
        </div>
    );
};
export default DepartmentDetails;
