
namespace NativeWebView.HTML.JavaScript.Base
{
    /// <summary>
    /// Different actions expected to be received from the update call backs.
    /// </summary>
    internal enum RegistrationAction
    {
        RegisterEvent, //Register to an event callback
        UnregisterEvent, //Unregister to an event callback
        AddElement, //Add an HTML element to the root element
        RemoveElement, //Remove an html element the root element
        UpdateProperty, //Update an attribute on an html property
        UpdateStyle, //Update a style attribute on an html property
        UpdateInnerText, //Updates thje content between the tags on a html element
        AddChild, //Add one or more children to a collection element
        RemoveChild, //remove one or more children to a collection element
    }
}
