import React, { Suspense, useLayoutEffect } from "react";
import { Switch, Route } from "react-router-dom";
import {
    Form,
    Field,
    FormElement,
    FieldRenderProps,
    FormRenderProps,
} from "@progress/kendo-react-form";
import { Error } from "@progress/kendo-react-labels";
import { Input } from "@progress/kendo-react-inputs";
import { DropDownList } from "@progress/kendo-react-dropdowns";

const StudentDetails = () => {
    useLayoutEffect(() => {
        window.scrollTo(0, 0);
    });
    const emailRegex = new RegExp(/\S+@\S+\.\S+/);
    const emailValidator = (value) =>
        emailRegex.test(value) ? "" : "Please enter a valid email.";
    const EmailInput = (fieldRenderProps) => {
        const { validationMessage, visited, ...others } = fieldRenderProps;
        return (
            <div>
                <Input {...others} />
                {visited && validationMessage && <Error>{validationMessage}</Error>}
            </div>
        );
    };
    const sizes = ["X-Small", "Small", "Medium", "Large", "X-Large", "2X-Large"];
    const handleSubmit = (dataItem) => alert(JSON.stringify(dataItem, null, 2));
    return (
        <div style={{ marginTop: 65, position: "fixed" }} class="col-md-4">
            <h4>Student Details</h4>
            <Form style={{ outerWidth: 500 }}
                onSubmit={handleSubmit}
                render={(formRenderProps) => (
                    <FormElement>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                First Name</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"firstName"}
                                    component={Input} />
                            </div>
                        </div>
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
                                <Field
                                    name={"email"}
                                    type={"email"}
                                    component={EmailInput}
                                    validator={emailValidator}
                                />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                DateOfBirth</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"dateofbirth"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                CGPA/Percentage</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"cgpaorpercentage"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Contact</label>
                            <div class="form-control-wrap">
                                <Field
                                    name={"contact"}
                                    component={Input} />
                            </div>
                        </div>
                        <div class="form-group" style={{ marginBottom: 15 }}>
                            <label class="form-label" for="name">
                                Department</label>
                            <div class="form-control-wrap">
                                <DropDownList
                                    data={sizes} 
                                    style={{
                                        width: "300px",
                                    }}
                                />
                            </div>
                        </div>


                        <div className="k-form-buttons">
                            <button
                                type={"submit"}
                                className="k-button k-button-md k-rounded-md k-button-solid k-button-solid-base"
                                disabled={!formRenderProps.allowSubmit}
                            >
                                Submit
                            </button>
                        </div>
                    </FormElement>
                )}
            />
        </div>
    );
};
export default StudentDetails;
