import React from "react";
import Icon from "../../components/icon/Icon";

const HeaderSearch = () => {
  return (
    <React.Fragment>
      <Icon name="search"></Icon>
      <input className="form-control border-transparent form-focus-none" type="text" placeholder="Search anything" />
    </React.Fragment>
  );
};

export default HeaderSearch;
