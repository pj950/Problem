﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class MsgCommon
    {

        public static string Show(string msg, string title = "提示", int timeout = 3000, string showType = "slide")
        {
            return string.Format("<script type='text/javascript'>$.messager.show({{title:'{0}',msg:'{1}',timeout:{2},showType:'{3}'}});</script>"
                , title, msg, timeout.ToString(), showType);
        }

        /// <summary>
        /// 获取ajax请求返回成功消息的JSON格式的字符串
        /// </summary>
        /// <returns></returns>
        public static string respJsonSuccessMsg()
        {
            return "{\"success\":true}";
        }

        /// <summary>
        /// 获取ajax请求返回成功消息的JSON格式的字符串
        /// </summary>
        /// <returns></returns>
        public static string RespJsonSuccessMsg(string Msg)
        {
            return "{\"success\":true,\"Msg\":\"" + Msg + "\"}";
        }

        /// <summary>
        /// 获取ajax请求返回错误消息的JSON格式的字符串
        /// </summary>
        /// <param name="errMsg">错误消息</param>
        /// <returns></returns>
        public static string respJsonErrorMsg(string errMsg)
        {
            return "{\"success\":false,\"errorMsg\":\"" + errMsg + "\"}";
        }


        /// <summary>
        /// 弹出信息,并跳转指定页面。
        /// </summary>
        public static void AlertAndRedirect(string message, string toURL)
        {
            //string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            string js = "<script language=javascript>alert('{0}');window.location.href='{1}'</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出信息,并返回历史页面
        /// </summary>
        public static void AlertAndGoHistory(string message, int value)
        {
            string js = @"<Script language='JavaScript'>alert('{0}');history.go({1});</Script>";
            HttpContext.Current.Response.Write(string.Format(js, message, value));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 直接跳转到指定的页面
        /// </summary>
        public static void Redirect(string toUrl)
        {
            string js = @"<script language=javascript>window.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, toUrl));
        }

        /// <summary>
        /// 弹出信息 并指定到父窗口
        /// </summary>
        public static void AlertAndParentUrl(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.top.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }

        /// <summary>
        /// 返回到父窗口
        /// </summary>
        public static void ParentRedirect(string ToUrl)
        {
            string js = "<script language=javascript>window.top.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, ToUrl));
        }

        /// <summary>
        /// 返回历史页面
        /// </summary>
        public static void BackHistory(int value)
        {
            string js = @"<Script language='JavaScript'>history.go({0});</Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 弹出信息
        /// </summary>
        public static void Alert(string message)
        {
            string js = "<script language=javascript>alert('{0}');</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }

        /// <summary>
        /// 注册脚本块
        /// </summary>
        public static void RegisterScriptBlock(System.Web.UI.Page page, string _ScriptString)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock", "<script type='text/javascript'>" + _ScriptString + "</script>");
        }
    }
}