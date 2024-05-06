"use strict";
 var options = { month: 'long', day: 'numeric', year: 'numeric' };
let direction = "ltr";
isRtl && (direction = "rtl"),

    document.addEventListener("DOMContentLoaded", function () {
        {
            const v = document.getElementById("calendar"),
                m = document.querySelector(".app-calendar-sidebar"),
                p = document.getElementById("addEventSidebar"), //----
                f = document.querySelector(".app-overlay"),
                g = {
                    Business: "primary",
                    available: "success",
                    not_available: "danger",
                    Family: "warning",
                    ETC: "info",
                },
                b = document.querySelector(".offcanvas-title"),
                h = document.querySelector(".btn-toggle-sidebar"), //--------------
                y = document.querySelector('button[type="submit"]'),
                S = document.querySelector(".btn-delete-event"),
                L = document.querySelector(".btn-cancel"),
                E = document.querySelector("#eventTitle"),
                k = document.querySelector("#eventStartDate"),
               
               
                w = document.querySelector("#eventEndDate"),
                x = document.querySelector("#eventURL"),
                q = $("#eventLabel"),
                D = $("#eventGuests"),
                P = document.querySelector("#eventLocation"),
                M = document.querySelector("#eventDescription"),
                T = document.querySelector(".allDay-switch"),
                A = document.querySelector(".select-all"),
                F = [].slice.call(document.querySelectorAll(".input-filter")),
                abc =document.getElementById("eventStartDate"),
                Y = document.querySelector(".inline-calendar"),
             
                ID =  document.querySelector(".getId"),



                SD =  document.querySelector(".SchedDate");

            let a,
                l = events,
                r = !1,
                e;
            const C = new bootstrap.Offcanvas(p);
            function t(e) {
                return e.id
                    ? "<span class='badge badge-dot bg-" +
                    $(e.element).data("label") +
                    " me-2'> </span>" +
                    e.text
                    : e.text;
            }
            function n(e) {
                return e.id
                    ? "<div class='d-flex flex-wrap align-items-center'><div class='avatar avatar-xs me-2'><img src='" +
                    assetsPath +
                    "img/avatars/" +
                    $(e.element).data("avatar") +
                    "' alt='avatar' class='rounded-circle' /></div>" +
                    e.text +
                    "</div>"
                    : e.text;
            }
            var d, o;
            function s() {
                var e = document.querySelector(".fc-sidebarToggle-button");
                for (
                    e.classList.remove("fc-button-primary"),
                    e.classList.add("d-lg-none", "d-inline-block", "ps-0");
                    e.firstChild;

                )
                    e.firstChild.remove();
                e.setAttribute("data-bs-toggle", "sidebar"),
                    e.setAttribute("data-overlay", ""),
                    e.setAttribute("data-target", "#app-calendar-sidebar"),
                    e.insertAdjacentHTML(
                        "beforeend",
                        '<i class="bx bx-menu bx-sm text-heading"></i>',
                    );
            }
            q.length &&
                q.wrap('<div class="position-relative"></div>').select2({
                    placeholder: "Select value",
                    dropdownParent: q.parent(),
                    templateResult: t,
                    templateSelection: t,
                    minimumResultsForSearch: -1,
                    escapeMarkup: function (e) {
                        return e;
                    },
                }),
                D.length &&
                D.wrap('<div class="position-relative"></div>').select2({
                    placeholder: "Select value",
                    dropdownParent: D.parent(),
                    closeOnSelect: !1,
                    templateResult: n,
                    templateSelection: n,
                    escapeMarkup: function (e) {
                        return e;
                    },
                }),
                //k &&
                //(d = k.flatpickr({
                //    enableTime: false,
                //    altFormat: "Y-m-dTH:i:S",
                //    onReady: function (e, t, n) {
                //        n.isMobile && n.mobileInput.setAttribute("step", null);
                //    },
                //})),
                w &&
                (o = w.flatpickr({
                    enableTime: !0,
                    altFormat: "Y-m-dTH:i:S",
                    onReady: function (e, t, n) {
                        n.isMobile && n.mobileInput.setAttribute("step", null);
                    },
                })),
                Y && (e = Y.flatpickr({ monthSelectorType: "static", inline: !0 }));

            let i = new Calendar(v, {
                initialView: "dayGridMonth",
                events: function (e, t) {
                    let n = (function () {
                        let t = [],
                            e = [].slice.call(
                                document.querySelectorAll(".input-filter:checked"),
                            );
                        return (
                            e.forEach((e) => {
                                t.push(e.getAttribute("data-value"));
                        
                            }),
                            t
                        );
                    })();
                    t(
                        l.filter(function (e) {
                            return n.includes(e.extendedProps.calendar.toLowerCase());
                        }),
                    );
                },
                plugins: [dayGridPlugin, interactionPlugin, listPlugin, timegridPlugin],
                editable: !0,
                dragScroll: !0,
                dayMaxEvents: 2,
                eventResizableFromStart: !0,
                customButtons: { sidebarToggle: { text: "Sidebar" } },
                headerToolbar: {
                    start: "sidebarToggle, prev,next, title",
                    end: "dayGridMonth,timeGridWeek,timeGridDay,listMonth",
                },
                direction: direction,
                initialDate: new Date(),
                navLinks: !0,
                eventClassNames: function ({ event: e }) {
                    return ["fc-event-" + g[e._def.extendedProps.calendar]];
                },

                dateClick: function (e) {
                    e = moment(e.date).format("YYYY-MM-DD");
                    u(),
                    
                    /*
                        
                      C.show(),
                      b && (b.innerHTML = "Add Event"),
                        (y.innerHTML = "Add"),
                        y.classList.remove("btn-update-event"),
                        y.classList.add("btn-add-event"),
                        (k.value = e),
                     
                    
                       
                    */
                        
                   
                        (w.value = e);
                },
                //------------------

                eventClick: function (e) {
             
                    (e = e),
                        (a = e.event).url && (e.jsEvent.preventDefault(), window.open(a.url, "_blank")),
                          (ID.value = a.id);
                        C.show(),
                       
                        b && (b.innerHTML = SD.innerHTML = new Date(a.start).toLocaleString('en-US', { month: 'long', day: 'numeric', year: 'numeric' })),
                        SD && (SD.innerHTML = "Date: "+new Date(a.start).toLocaleString('en-US', { month: 'long', day: 'numeric', year: 'numeric' }) + 
                        "<br>" + a.title  + "<br>" + (a.extendedProps && a.extendedProps.calendar)),


                        (y.innerHTML = "Update"),
                       //-------------------------------------------------------------------
                        y.classList.add("btn-update-event"),
                        y.classList.remove("btn-add-event"),
                        k.classList.add("d-none"),
                       
                         //(ID.value = a.id),
                         (ID.value = parseInt(a.id, 10)),
                        d.setDate(a.start, !0, "Y-m-d"),
                        !0 === a.allDay ? (T.checked = !0) : (T.checked = !1),
                        null !== a.end
                            ? o.setDate(a.end, !0, "Y-m-d")
                            : o.setDate(a.start, !0, "Y-m-d"),
                        q.val(a.extendedProps.calendar).trigger("change"),
                        void 0 !== a.extendedProps.location &&
                        (P.value = a.extendedProps.location),
                        void 0 !== a.extendedProps.guests &&
                        D.val(a.extendedProps.guests).trigger("change"),
                        void 0 !== a.extendedProps.description &&
                        (M.value = a.extendedProps.description);
                },
                datesSet: function () {
                    s();
                },
                viewDidMount: function () {
                    s();
                },
            });


            i.render(), s();
            var c = document.getElementById("eventForm");
            function u() {
                (w.value = ""),
                    (x.value = ""),
                    (k.value = ""),
                    (E.value = ""),
                    (P.value = ""),
                    (T.checked = !1),
                    D.val("").trigger("change"),
                    (M.value = "");
            }
            FormValidation.formValidation(c, {
                fields: {
                    eventTitle: {
                        validators: { notEmpty: { message: "Please enter event title " } },
                    },
                    eventStartDate: {
                        validators: { notEmpty: { message: "Please enter start date " } },
                    },
                    eventEndDate: {
                        validators: { notEmpty: { message: "Please enter end date " } },
                    },
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap5: new FormValidation.plugins.Bootstrap5({
                        eleValidClass: "",
                        rowSelector: function (e, t) {
                            return ".mb-3";
                        },
                    }),
                    submitButton: new FormValidation.plugins.SubmitButton(),
                    autoFocus: new FormValidation.plugins.AutoFocus(),
                },
            })
                .on("core.form.valid", function () {
                    r = !0;
                })
                .on("core.form.invalid", function () {
                    r = !1;
                }),
                h &&
                h.addEventListener("click", (e) => {
                    L.classList.remove("d-none");
                }),
                y.addEventListener("click", (e) => {
                    var t, n;
                    y.classList.contains("btn-add-event")
                        ? r &&
                        ((n = {
                            id: i.getEvents().length + 1,
                            title: E.value,
                            start: k.value,
                            end: w.value,
                            startStr: k.value,
                            endStr: w.value,
                            display: "block",
                            extendedProps: {
                                location: P.value,
                                guests: D.val(),
                                calendar: q.val(),
                                description: M.value,
                            },
                        }),
                            x.value && (n.url = x.value),
                            T.checked && (n.allDay = !0),
                            (n = n),
                            l.push(n),
                            i.refetchEvents(),
                            C.hide())

                        : r &&
                        ((n = {
                            id: a.id,
                            title: E.value,
                            start: k.value,
                            end: w.value,
                            url: x.value,
                            extendedProps: {
                                location: P.value,
                                guests: D.val(),
                                calendar: q.val(),
                                description: M.value,
                            },
                            display: "block",
                            allDay: !!T.checked,
                        }),
                            ((t = n).id = parseInt(t.id)),
                            (l[l.findIndex((e) => e.id === t.id)] = t),
                            i.refetchEvents(),
                            C.hide());
                }),
                S.addEventListener("click", (e) => {
                    var t;
                    (t = parseInt(a.id)),
                        (l = l.filter(function (e) {
                            return e.id != t;
                        })),
                        i.refetchEvents(),
                        C.hide();
                }),
                p.addEventListener("hidden.bs.offcanvas", function () {
                    u();
                }),

                h.addEventListener("click", (e) => {
                    b && (b.innerHTML = "Add Event"),
                        (y.innerHTML = "Add"),
                        y.classList.remove("btn-update-event"),
                        y.classList.add("btn-add-event"),

                        S.classList.add("d-none"),
                        m.classList.remove("show"),

                        f.classList.remove("show");

                       
                }),
                A &&
                A.addEventListener("click", (e) => {
                    e.currentTarget.checked
                        ? document
                            .querySelectorAll(".input-filter")
                            .forEach((e) => (e.checked = 1))
                        : document
                            .querySelectorAll(".input-filter")
                            .forEach((e) => (e.checked = 0)),
                        i.refetchEvents();
                }),
                F &&
                F.forEach((e) => {
                    e.addEventListener("click", () => {
                        document.querySelectorAll(".input-filter:checked").length <
                            document.querySelectorAll(".input-filter").length
                            ? (A.checked = !1)
                            : (A.checked = !0),
                            i.refetchEvents();
                    });
                }),
                e.config.onChange.push(function (e) {
                    i.changeView(i.view.type, moment(e[0]).format("YYYY-MM-DD")),
                        s(),
                        m.classList.remove("show"),
                        f.classList.remove("show");
                });
        }
    });


//"use strict";let direction="ltr";isRtl&&(direction="rtl"),document.addEventListener("DOMContentLoaded",function(){{const v=document.getElementById("calendar"),m=document.querySelector(".app-calendar-sidebar"),p=document.getElementById("addEventSidebar"),f=document.querySelector(".app-overlay"),g={Business:"primary",Holiday:"success",Personal:"danger",Family:"warning",ETC:"info"},b=document.querySelector(".offcanvas-title"),h=document.querySelector(".btn-toggle-sidebar"),y=document.querySelector('button[type="submit"]'),S=document.querySelector(".btn-delete-event"),L=document.querySelector(".btn-cancel"),E=document.querySelector("#eventTitle"),k=document.querySelector("#eventStartDate"),w=document.querySelector("#eventEndDate"),x=document.querySelector("#eventURL"),q=$("#eventLabel"),D=$("#eventGuests"),P=document.querySelector("#eventLocation"),M=document.querySelector("#eventDescription"),T=document.querySelector(".allDay-switch"),A=document.querySelector(".select-all"),F=[].slice.call(document.querySelectorAll(".input-filter")),Y=document.querySelector(".inline-calendar");let a,l=events,r=!1,e;const C=new bootstrap.Offcanvas(p);function t(e){return e.id?"<span class='badge badge-dot bg-"+$(e.element).data("label")+" me-2'> </span>"+e.text:e.text}function n(e){return e.id?"<div class='d-flex flex-wrap align-items-center'><div class='avatar avatar-xs me-2'><img src='"+assetsPath+"img/avatars/"+$(e.element).data("avatar")+"' alt='avatar' class='rounded-circle' /></div>"+e.text+"</div>":e.text}var d,o;function s(){var e=document.querySelector(".fc-sidebarToggle-button");for(e.classList.remove("fc-button-primary"),e.classList.add("d-lg-none","d-inline-block","ps-0");e.firstChild;)e.firstChild.remove();e.setAttribute("data-bs-toggle","sidebar"),e.setAttribute("data-overlay",""),e.setAttribute("data-target","#app-calendar-sidebar"),e.insertAdjacentHTML("beforeend",'<i class="bx bx-menu bx-sm text-heading"></i>')}q.length&&q.wrap('<div class="position-relative"></div>').select2({placeholder:"Select value",dropdownParent:q.parent(),templateResult:t,templateSelection:t,minimumResultsForSearch:-1,escapeMarkup:function(e){return e}}),D.length&&D.wrap('<div class="position-relative"></div>').select2({placeholder:"Select value",dropdownParent:D.parent(),closeOnSelect:!1,templateResult:n,templateSelection:n,escapeMarkup:function(e){return e}}),k&&(d=k.flatpickr({enableTime:false,altFormat:"Y-m-dTH:i:S",onReady:function(e,t,n){n.isMobile&&n.mobileInput.setAttribute("step",null)}})),w&&(o=w.flatpickr({enableTime:!0,altFormat:"Y-m-dTH:i:S",onReady:function(e,t,n){n.isMobile&&n.mobileInput.setAttribute("step",null)}})),Y&&(e=Y.flatpickr({monthSelectorType:"static",inline:!0}));let i=new Calendar(v,{initialView:"dayGridMonth",events:function(e,t){let n=function(){let t=[],e=[].slice.call(document.querySelectorAll(".input-filter:checked"));return e.forEach(e=>{t.push(e.getAttribute("data-value"))}),t}();t(l.filter(function(e){return n.includes(e.extendedProps.calendar.toLowerCase())}))},plugins:[dayGridPlugin,interactionPlugin,listPlugin,timegridPlugin],editable:!0,dragScroll:!0,dayMaxEvents:2,eventResizableFromStart:!0,customButtons:{sidebarToggle:{text:"Sidebar"}},headerToolbar:{start:"sidebarToggle, prev,next, title",end:"dayGridMonth,timeGridWeek,timeGridDay,listMonth"},direction:direction,initialDate:new Date,navLinks:!0,eventClassNames:function({event:e}){return["fc-event-"+g[e._def.extendedProps.calendar]]},dateClick:function(e){e=moment(e.date).format("YYYY-MM-DD");u(),C.show(),b&&(b.innerHTML="Add Event"),y.innerHTML="Add",y.classList.remove("btn-update-event"),y.classList.add("btn-add-event"),S.classList.add("d-none"),k.value=e,w.value=e},eventClick:function(e){e=e,(a=e.event).url&&(e.jsEvent.preventDefault(),window.open(a.url,"_blank")),C.show(),b&&(b.innerHTML="Update Event"),y.innerHTML="Update",y.classList.add("btn-update-event"),y.classList.remove("btn-add-event"),S.classList.remove("d-none"),E.value=a.title,d.setDate(a.start,!0,"Y-m-d"),!0===a.allDay?T.checked=!0:T.checked=!1,null!==a.end?o.setDate(a.end,!0,"Y-m-d"):o.setDate(a.start,!0,"Y-m-d"),q.val(a.extendedProps.calendar).trigger("change"),void 0!==a.extendedProps.location&&(P.value=a.extendedProps.location),void 0!==a.extendedProps.guests&&D.val(a.extendedProps.guests).trigger("change"),void 0!==a.extendedProps.description&&(M.value=a.extendedProps.description)},datesSet:function(){s()},viewDidMount:function(){s()}});i.render(),s();var c=document.getElementById("eventForm");function u(){w.value="",x.value="",k.value="",E.value="",P.value="",T.checked=!1,D.val("").trigger("change"),M.value=""}FormValidation.formValidation(c,{fields:{eventTitle:{validators:{notEmpty:{message:"Please enter event title "}}},eventStartDate:{validators:{notEmpty:{message:"Please enter start date "}}},eventEndDate:{validators:{notEmpty:{message:"Please enter end date "}}}},plugins:{trigger:new FormValidation.plugins.Trigger,bootstrap5:new FormValidation.plugins.Bootstrap5({eleValidClass:"",rowSelector:function(e,t){return".mb-3"}}),submitButton:new FormValidation.plugins.SubmitButton,autoFocus:new FormValidation.plugins.AutoFocus}}).on("core.form.valid",function(){r=!0}).on("core.form.invalid",function(){r=!1}),h&&h.addEventListener("click",e=>{L.classList.remove("d-none")}),y.addEventListener("click",e=>{var t,n;y.classList.contains("btn-add-event")?r&&(n={id:i.getEvents().length+1,title:E.value,start:k.value,end:w.value,startStr:k.value,endStr:w.value,display:"block",extendedProps:{location:P.value,guests:D.val(),calendar:q.val(),description:M.value}},x.value&&(n.url=x.value),T.checked&&(n.allDay=!0),n=n,l.push(n),i.refetchEvents(),C.hide()):r&&(n={id:a.id,title:E.value,start:k.value,end:w.value,url:x.value,extendedProps:{location:P.value,guests:D.val(),calendar:q.val(),description:M.value},display:"block",allDay:!!T.checked},(t=n).id=parseInt(t.id),l[l.findIndex(e=>e.id===t.id)]=t,i.refetchEvents(),C.hide())}),S.addEventListener("click",e=>{var t;t=parseInt(a.id),l=l.filter(function(e){return e.id!=t}),i.refetchEvents(),C.hide()}),p.addEventListener("hidden.bs.offcanvas",function(){u()}),h.addEventListener("click",e=>{b&&(b.innerHTML="Add Event"),y.innerHTML="Add",y.classList.remove("btn-update-event"),y.classList.add("btn-add-event"),S.classList.add("d-none"),m.classList.remove("show"),f.classList.remove("show")}),A&&A.addEventListener("click",e=>{e.currentTarget.checked?document.querySelectorAll(".input-filter").forEach(e=>e.checked=1):document.querySelectorAll(".input-filter").forEach(e=>e.checked=0),i.refetchEvents()}),F&&F.forEach(e=>{e.addEventListener("click",()=>{document.querySelectorAll(".input-filter:checked").length<document.querySelectorAll(".input-filter").length?A.checked=!1:A.checked=!0,i.refetchEvents()})}),e.config.onChange.push(function(e){i.changeView(i.view.type,moment(e[0]).format("YYYY-MM-DD")),s(),m.classList.remove("show"),f.classList.remove("show")})}}); 