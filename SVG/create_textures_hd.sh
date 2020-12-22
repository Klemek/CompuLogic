#!/bin/bash
ls Cursor | sed -e 's/\.svg$//'|xargs -I % sh -c 'svgexport Cursor/%.svg ../Assets/Textures/Cursor/%.png pad 4096:'
ls UI | sed -e 's/\.svg$//'|xargs -I % sh -c 'svgexport UI/%.svg ../Assets/Textures/UI/%.png pad 4096'
ls Gates | sed -e 's/\.svg$//'|xargs -I % sh -c 'svgexport Gates/%.svg ../Assets/Textures/Gates/%.png pad 8x'
