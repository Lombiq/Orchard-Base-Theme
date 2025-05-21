if (typeof window.lombiqBaseThemeGridBreakpoints != 'object') {
    window.lombiqBaseThemeGridBreakpoints = {
        xs: 0,
        sm: 576,
        md: 768,
        lg: 992,
        xl: 1200,
        xxl: 1400,
    };
}

function addResizeObserver() {
    if (window.lombiqBaseThemeGridBreakpointsObserver) return;

    window.lombiqBaseThemeGridBreakpointsObserver = new ResizeObserver((entries) => {
        const target = entries[0].target;
        const width = target.clientWidth;
        const breakpointsDescending = Object
            .entries(window.lombiqBaseThemeGridBreakpoints)
            .sort(([_, a], [__, b]) => b - a);
        const size = breakpointsDescending.filter(([_, size]) => size < width)[0][0];

        if (window.lombiqBaseThemeGridBreakpointsCurrentSize === size) return;

        window.lombiqBaseThemeGridBreakpointsCurrentSize = size;
        console.log(`Size changed: ${width}(${size})`);

        target.classList.remove(...Array.from(target.classList).filter(name => name.startsWith('breakpoint-')));
        target.classList.add('breakpoint-' + size);

        const names = breakpointsDescending.map(([name, _]) => name);
        const index = names.indexOf(size);
        const smaller = names.slice(index);
        const larger = names.slice(0, index + 1);
        const rangeClasses = smaller.flatMap((small) => larger.map((large) => `breakpoint-${small}-${large}`));
        target.classList.add(...rangeClasses);
    });

    window.lombiqBaseThemeGridBreakpointsObserver.observe(document.body);
}

document.addEventListener('DOMContentLoaded', addResizeObserver, false);
