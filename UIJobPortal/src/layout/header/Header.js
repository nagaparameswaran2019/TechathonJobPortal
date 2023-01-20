import React from "react";
import classNames from "classnames";
import Toggle from "../sidebar/Toggle"; 
import User from "./dropdown/user/User";
 
 

const Header = ({ fixed, theme, className, setVisibility, ...props }) => {
  const headerClass = classNames({
    "nk-header": true,
    "nk-header-fixed": fixed,
    [`is-light`]: theme === "white",
    [`is-${theme}`]: theme !== "white" && theme !== "light",
    [`${className}`]: className,
  });
  return (
    <div className={headerClass}>
      <div className="container-fluid">
        <div className="nk-header-wrap">
          <div className="nk-menu-trigger d-xl-none ml-n1">
            
          </div>
          <div className="nk-header-brand d-xl-none">
           
          </div> 
          <div className="nk-header-tools">
            <ul className="nk-quick-nav">
              <li className="chats-dropdown hide-mb-xs"  onClick={() => setVisibility(false)}>
                 
              </li>
              <li className="notification-dropdown mr-n1"  onClick={() => setVisibility(false)}>
               
              </li>
              <li className="user-dropdown"  onClick={() => setVisibility(false)}>
                <User />
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Header;
